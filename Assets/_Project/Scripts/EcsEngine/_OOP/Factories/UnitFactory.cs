using System;
using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Tags;
using _Project.Scripts.EcsEngine.Enums;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.EcsEngine._OOP.Factories
{
    public class UnitFactory : IInitializable
    {
        private const string PREFAB_KEY = "unit";
        private const int POOL_SIZE = 10;
        private readonly EntityManager _entityManager;
        private readonly PrefabProvider _prefabProvider;
        private readonly GameBalance _balance;
        
        private Pool<Entity> _units;
        
        [Inject]
        public UnitFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance)
        {
            _entityManager = entityManager;
            _prefabProvider = prefabProvider;
            _balance = balance;
        }

        public void Initialize()
        {
            var prefab = _prefabProvider.GetPrefab(PREFAB_KEY);
            _units = new Pool<Entity>(prefab, POOL_SIZE);
        }
        
        public async UniTaskVoid Spawn(UnitType type, TeamType teamType, Vector3 position, Quaternion rotation,
            Transform parent = null)
        {
            var config = _balance.GetUnitConfigByClass(type);
            
            var entity = _units.Spawn();
            entity.Initialize(_entityManager.GetWorld());
            
            entity.AddData(new TransformView{Value = entity.transform});
            entity.AddData(new UnitTag());
            entity.AddData(new Team{Value = teamType});
            entity.AddData(new UnitClass{Value = type});
            entity.AddData(new Position{Value = position});
            entity.AddData(new Rotation{Value = rotation});
            entity.AddData(new MoveSpeed{Value = config.MoveSpeed});
            entity.AddData(new Health{Value = config.Health});
            _entityManager.Register(entity);

            await UniTask.Delay(TimeSpan.FromSeconds(2));
            
            //entity.AddData(new MoveDirection{Value = Vector3.forward});
            entity.AddData(new MoveDirection());
        }
        
        public void DeSpawn(int id)
        {
            var entity = _entityManager.UnRegister(id);
            _units.DeSpawn(entity);
        }
    }
}
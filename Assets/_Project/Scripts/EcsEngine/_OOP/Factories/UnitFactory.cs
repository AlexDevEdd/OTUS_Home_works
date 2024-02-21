using System;
using System.Collections.Generic;
using System.Text;
using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using _Project.Scripts.EcsEngine.Enums;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.EcsEngine._OOP.Factories
{
    [UsedImplicitly]
    public class UnitFactory : IInitializable
    {
        private readonly EntityManager _entityManager;
        private readonly GameBalance _balance;
        private readonly IEnumerable<IConcreteUnitFactory> _factories;

        private readonly Dictionary<string, IConcreteUnitFactory> _unitFactories = new ();
        private readonly HashSet<Entity> _activeUnits = new ();
      
        public IReadOnlyCollection<Entity> ActiveUnits => _activeUnits;

        
        public UnitFactory(EntityManager entityManager, GameBalance balance, IEnumerable<IConcreteUnitFactory> factories)
        {
            _entityManager = entityManager;
            _balance = balance;
            _factories = factories;
        }
        
        public void Initialize()
        {
            foreach (var factory in _factories) 
                _unitFactories.TryAdd(factory.PrefabKey, factory);
        }
        
        public async UniTaskVoid Spawn(UnitType type, TeamType teamType, Vector3 position, Quaternion rotation)
        {
            var config = _balance.GetUnitConfigByClass(type);
            if (_unitFactories.TryGetValue(ConvertToKey(type, teamType), out var factory))
            {
                var entity = factory.Spawn();
                entity.Initialize(_entityManager.GetWorld());
                entity.WithData(new TransformView{Value = entity.transform})
                    .WithData(new UnitTag())
                    .WithData(new Team{Value = teamType})
                    .WithData(new UnitClass{Value = type})
                    .WithData(new Position{Value = position})
                    .WithData(new Rotation{Value = rotation})
                    .WithData(new MoveSpeed{Value = config.MoveSpeed})
                    .WithData(new RotationSpeed{Value = config.RotationSpeed})
                    .WithData(new Health{Value = config.Health})
                    .WithData(new TargetEntity())
                    .WithData(new AttackDistance{Value = config.AttackDistance})
                    .WithData(new AttackCoolDown{CurrentValue = 0f, OriginValue = config.AttackDelay});
            
                _entityManager.Register(entity);
            
                _activeUnits.Add(entity);
            
                await UniTask.Delay(TimeSpan.FromSeconds(2));
                entity.WithData(new FindTargetRequest());
            }
        }
        
        public void DeSpawn(int id)
        {
            var entity = _entityManager.Get(id);
            var unitType = entity.GetData<UnitClass>();
            var teamType = entity.GetData<Team>();
            
            if (_unitFactories.TryGetValue(ConvertToKey(unitType.Value, teamType.Value), out var factory))
            {
                _activeUnits.Remove(entity);
                factory.DeSpawn(entity.Id); 
            }
        }

        private string ConvertToKey(UnitType type, TeamType teamType)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Enum.GetName(typeof(UnitType), type));
            stringBuilder.Append(Enum.GetName(typeof(TeamType), teamType));
            return stringBuilder.ToString();
        }
    }

    public abstract class BaseUnitFactory : IConcreteUnitFactory
    {
        private readonly PrefabProvider _prefabProvider;
       // private readonly GameBalance _balance;
        private readonly EntityManager _entityManager;
        public string PrefabKey { get; private set; }
        protected abstract int FactoryIndex { get; }
        
        private readonly Pool<Entity> _units;
        
        public BaseUnitFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance)
        {
            _entityManager = entityManager;
            //_prefabProvider = prefabProvider;
            //_balance = balance;
            
            var config = balance.UnitFactoryConfigs[FactoryIndex];
            PrefabKey = config.PrefabKey;
            
            var prefab = prefabProvider.GetPrefab(PrefabKey);
            _units = new Pool<Entity>(prefab, config.PoolSize);
        }
        
        public Entity Spawn()
        {
           return _units.Spawn();
        }

        public void DeSpawn(int id)
        {
            var entity = _entityManager.UnRegister(id);
            _units.DeSpawn(entity);
        }
    }

    public interface IConcreteUnitFactory
    {
        public string PrefabKey { get; }
        public Entity Spawn();
        public void DeSpawn(int id);
    }

    [UsedImplicitly]
    public sealed class WarriorRedFactory : BaseUnitFactory
    {
        protected override int FactoryIndex => 0;


        public WarriorRedFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance) 
            : base(entityManager, prefabProvider, balance) { }
    }
    
    [UsedImplicitly]
    public sealed class WarriorBlueFactory : BaseUnitFactory
    {
        protected override int FactoryIndex => 1;

        public WarriorBlueFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance) 
            : base(entityManager, prefabProvider, balance) { }
    }
    
    
    [UsedImplicitly]
    public sealed class ArcherRedFactory : BaseUnitFactory
    {
        protected override int FactoryIndex => 2;

        public ArcherRedFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance) 
            : base(entityManager, prefabProvider, balance) { }
    }
    
    [UsedImplicitly]
    public sealed class ArcherBlueFactory : BaseUnitFactory
    {
        protected override int FactoryIndex => 3;

        public ArcherBlueFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance) 
            : base(entityManager, prefabProvider, balance) { }
    }
}
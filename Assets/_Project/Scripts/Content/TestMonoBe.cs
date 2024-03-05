using _Game.Scripts.Tools;
using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine._OOP.UI;
using _Project.Scripts.EcsEngine._OOP.UI.Views;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using _Project.Scripts.EcsEngine.Enums;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Content
{
    public class TestMonoBe : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        
        [SerializeField] private UnitType _type;
        [SerializeField] private TeamType _teamType;
        [SerializeField] private float _prepareTime = 3f;
        
        [Space] [SerializeField] private DamageTextPopUp textPopUp;
        
        private UnitSystem _unitSystem;
        private EntityManager _manager;
        private EcsAdmin _admin;
        
        [Inject]
        public void Construct(UnitSystem factory, EntityManager manager, EcsAdmin admin)
        {
            _unitSystem = factory;
            _manager = manager;
            _admin = admin;
        }

        [Button]
        public void Check()
        {
            var spawners = _manager.GetComponents<SpawnerTag>();
            
            foreach (var spawnerTag in spawners)
            {
                Log.ColorLog($"{spawnerTag.GetType().Name}");
            }
        }
        
        [Button]
        public void AddSpawnRequest()
        {
          
                _admin.CreateEntity(EcsWorlds.Events)
                .Add(new UnitSpawnRequest
                {
                    TeamType = _teamType,
                    UnitType = _type
                });
            // _entity.AddData(new UnitSpawnRequest
            // {
            //     UnitType = _type,
            //     TeamType = _teamType,
            //     //Position = transform.position,
            //    // Rotation = transform.rotation,
            //     //PrepareTime = _prepareTime
            // });
        }
        
       [Button]
        public void AddTAG(Entity entity)
        {
            entity.AddData(new UnitSpawnRequest
            {
                UnitType = _type,
                TeamType = _teamType,
               // Position = transform.localPosition,
               // Rotation = transform.rotation
            });
        }
        
        [Button]
        public void Spawn()
        {
            _unitSystem.Spawn(_type, _teamType, transform.localPosition, transform.rotation).Forget();
        }
    }
}
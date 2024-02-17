using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine._OOP.Systems;
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
        
        private UnitSystem _unitSystem;
        
        [Inject]
        public void Construct(UnitSystem factory)
        {
            _unitSystem = factory;
        }
        
        [Button]
        public void AddSpawnRequest()
        {
            _entity.AddData(new UnitSpawnRequest
            {
                UnitType = _type,
                TeamType = _teamType,
                Position = transform.localPosition,
                Rotation = transform.rotation
            });
        }
        
       [Button]
        public void AddTAG(Entity entity)
        {
            entity.AddData(new UnitSpawnRequest
            {
                UnitType = _type,
                TeamType = _teamType,
                Position = transform.localPosition,
                Rotation = transform.rotation
            });
        }
        
        [Button]
        public void Spawn()
        {
            _unitSystem.Spawn(_type, _teamType, transform.localPosition, transform.rotation);
        }
    }
}
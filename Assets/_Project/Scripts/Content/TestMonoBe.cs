using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine._OOP.UI;
using _Project.Scripts.EcsEngine._OOP.UI.Views;
using _Project.Scripts.EcsEngine.Components.Events;
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
        
        [Inject]
        public void Construct(UnitSystem factory)
        {
            _unitSystem = factory;
        }

        [Button]
        public void ShowText(string text, Transform pos)
        {
            textPopUp.Show(text, pos.position);
        }
        
        [Button]
        public void AddSpawnRequest()
        {
            _entity.AddData(new UnitSpawnRequest
            {
                UnitType = _type,
                TeamType = _teamType,
                Position = transform.position,
                Rotation = transform.rotation,
                PrepareTime = _prepareTime
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
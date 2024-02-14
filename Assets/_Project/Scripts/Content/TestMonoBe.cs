using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine._OOP.Factories;
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
        private UnitFactory _factory;
        [SerializeField] private UnitType _type;
        [SerializeField] private TeamType _teamType;
        
        [Inject]
        public void Construct(UnitFactory factory)
        {
            _factory = factory;
        }
        
       [Button]
        public void AddTAG(Entity entity)
        {
            entity.AddData(new Inactive());
        }
        
        [Button]
        public void Spawn()
        {
            _factory.Spawn(_type, _teamType, transform.localPosition, transform.rotation);
        }
    }
}
using System.Linq;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Enums;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.EcsEngine._OOP.Factories
{
    public sealed class UnitSystem
    {
        private readonly UnitFactory _unitFactory;
        
        [Inject]
        public UnitSystem(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public void Spawn(UnitType type, TeamType teamType, Vector3 position, Quaternion rotation,
            Transform parent = null)
        {
            _unitFactory.Spawn(type, teamType, position, rotation, parent).Forget();
           
        }

        public void DeSpawn(int id)
        {
            _unitFactory.DeSpawn(id);
        }

        public Entity GetClosestByTeam(TransformView ownerTransform, TeamType teamType)
        {
            var entity = _unitFactory.ActiveUnits.Where(e => e.GetData<Team>().Value == teamType)
                .OrderBy(e => Vector3.Distance(ownerTransform.Value.position, e.transform.position)).FirstOrDefault();
            
            return entity;
        }
    }
}
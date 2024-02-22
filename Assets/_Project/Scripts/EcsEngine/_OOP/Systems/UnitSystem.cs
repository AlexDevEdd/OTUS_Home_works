using System.Linq;
using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Enums;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.Systems
{
    [UsedImplicitly]
    public sealed class UnitSystem : ICustomInject
    {
        private readonly UnitFactory _unitFactory;
        
        public UnitSystem(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        public void Spawn(UnitType type, TeamType teamType, Vector3 position, Quaternion rotation)
        {
            _unitFactory.Spawn(type, teamType, position, rotation).Forget();
        }

        public void DeSpawn(int id)
        {
            _unitFactory.DeSpawn(id);
        }

        public bool IsHasTeam(TeamType teamType)
        {
            return _unitFactory.ActiveUnits.Any(u => u.GetData<Team>().Value == teamType);
        }

        public Entity GetClosestByTeam(TransformView ownerTransform, TeamType teamType)
        {
            var entity = _unitFactory.ActiveUnits
                .Where(e => e.GetData<Team>().Value == teamType)
                .OrderBy(e => Vector3.Distance(ownerTransform.Value.position, e.transform.position))
                .FirstOrDefault();
            
            return entity;
        }
    }
}
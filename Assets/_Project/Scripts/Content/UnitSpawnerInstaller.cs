using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Tags;
using _Project.Scripts.EcsEngine.Enums;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.Content
{
    public class UnitSpawnerInstaller : EntityInstaller
    {
        [SerializeField] private TeamType _teamType;
        [SerializeField] private UnitType _unitType;
        [SerializeField] private float _prepareTime;
        protected override void Install(Entity entity)
        {
            entity.WithData(new SpawnerTag())
                .WithData(new Position { Value = transform.position })
                .WithData(new Rotation { Value = transform.rotation })
                .WithData(new PrepareTime { Value = _prepareTime })
                .WithData(new UnitClass { Value = _unitType })
                .WithData(new Team { Value = _teamType });
        }

        protected override void Dispose(Entity entity)
        {
           
        }
    }
}
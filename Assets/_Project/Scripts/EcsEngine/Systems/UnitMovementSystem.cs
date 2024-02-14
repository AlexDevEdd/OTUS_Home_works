using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class UnitMovementSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position>, Exc<Inactive>> _moveFilter;

        private EcsCustomInject<UnitFactory> _unitFactory;
        
        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;

            var directionPool = _moveFilter.Pools.Inc1;
            var speedPool = _moveFilter.Pools.Inc2;
            var positionPool = _moveFilter.Pools.Inc3;

            foreach (var entity in _moveFilter.Value)
            {
                var moveDirection = directionPool.Get(entity);
                var moveSpeed = speedPool.Get(entity);
                ref var position = ref positionPool.Get(entity);

                position.Value += moveDirection.Value * moveSpeed.Value * deltaTime;
            }
        }
    }
}
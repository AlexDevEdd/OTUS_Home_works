using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class UnitMovementSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MoveSpeed, Position, TargetEntity>, Exc<Inactive>> _moveFilter;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        
        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            
            var speedPool = _moveFilter.Pools.Inc1;
            var positionPool = _moveFilter.Pools.Inc2;

            foreach (var entity in _moveFilter.Value)
            {
                var target = _targetPool.Value.Get(entity);
                if(target.Id == default)
                    continue;
                
                ref var position = ref positionPool.Get(entity);
                var moveSpeed = speedPool.Get(entity);

                var direction = target.Transform.position - position.Value;
                var distance = direction.magnitude;
                var normalizedDirection = direction / distance;
                
                if (distance > 1)
                    position.Value += normalizedDirection * moveSpeed.Value * deltaTime;
                else
                    position.Value = position.Value;
            }
        }
    }
}
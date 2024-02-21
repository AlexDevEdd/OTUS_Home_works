using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class UnitMovementSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MoveSpeed, Position, TargetEntity, AttackDistance>,
            Exc<Inactive, AttackRequest>> _moveFilter;
        
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        private readonly EcsPoolInject<AttackDistance> _attackDistancePool;
        private readonly EcsPoolInject<AttackRequest> _attackingRequestPool;
        
        private EcsCustomInject<EntityManager> _entityManager;
        
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
                
                if (distance > _attackDistancePool.Value.Get(entity).Value)
                    position.Value += normalizedDirection * moveSpeed.Value * deltaTime;
                else
                {
                    _attackingRequestPool.Value.Add(entity) = new AttackRequest{Target = target};
                }
            }
        }
    }
}
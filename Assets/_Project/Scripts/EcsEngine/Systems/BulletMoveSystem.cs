using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class BulletMoveSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MoveSpeed, Position, TargetEntity, MoveDirection, BulletTag>,
            Exc<Inactive>> _moveFilter;
        
        private readonly EcsPoolInject<MoveDirection> _moveDirectionPool;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        
        private EcsCustomInject<EntityManager> _entityManager;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            
            var speedPool = _moveFilter.Pools.Inc1;
            var positionPool = _moveFilter.Pools.Inc2;
            
            foreach (var entity in _moveFilter.Value)
            {
                ref var moveDirection = ref _moveDirectionPool.Value.Get(entity); 
                var target = _targetPool.Value.Get(entity);
                
                ref var position = ref positionPool.Get(entity);
                var moveSpeed = speedPool.Get(entity);

                var targetPos = target.Transform.position;
                targetPos.y += 3;
                var direction = targetPos - position.Value;
                var distance = direction.magnitude;
                var normalizedDirection = direction / distance;
                
                moveDirection.Value = normalizedDirection * moveSpeed.Value * deltaTime;
                position.Value += normalizedDirection * moveSpeed.Value * deltaTime;
            }
        }
    }
}
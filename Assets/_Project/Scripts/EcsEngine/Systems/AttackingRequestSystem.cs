﻿using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class AttackingRequestSystem : IEcsRunSystem
    {
        private const float  RESET_VALUE = 0f;
        private const float  EMPTY_HEALTH_VALUE = 0f;
        
        private readonly EcsFilterInject<Inc<AttackRequest, TransformView, TargetEntity, AttackCoolDown>, 
            Exc<FindTargetRequest>> _filter;
        
        private readonly EcsPoolInject<FindTargetRequest> _findTargetRequestPool;
        private readonly EcsPoolInject<AttackCoolDown> _attackCoolDownPool;
        private readonly EcsPoolInject<AttackRequest> _attackRequestPool;
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool;
        private readonly EcsPoolInject<AttackEvent> _attackEventPool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<Reached> _reachedPool;
        
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            
            foreach (var entity in _filter.Value)
            {
                if(!_reachedPool.Value.Get(entity).IsReached)
                    continue;
                
                var request = _attackRequestPool.Value.Get(entity);
                ref var coolDown = ref _attackCoolDownPool.Value.Get(entity);
                
                if (_world.Value.IsEntityAlive(request.Target.Id))
                {
                    var targetHealth = _healthPool.Value.Get(request.Target.Id);

                    if (targetHealth.Value <= EMPTY_HEALTH_VALUE)
                    {
                        _targetEntityPool.Value.Del(entity);
                        _attackRequestPool.Value.Del(entity);
                        _findTargetRequestPool.Value.Add(entity) = new FindTargetRequest();
                        ref var isReached = ref _reachedPool.Value.Get(entity);
                        isReached.IsReached = false;
                    
                        coolDown.CurrentValue = RESET_VALUE;
                        continue;
                    } 
                    
                    coolDown.CurrentValue -= deltaTime;

                    if (targetHealth.Value > EMPTY_HEALTH_VALUE && coolDown.CurrentValue <= RESET_VALUE)
                    {
                        var transform = _transformViewPool.Value.Get(entity);
                        _attackEventPool.Value.Add(entity) = new AttackEvent
                        {
                            SourceEntity = new SourceEntity
                            {
                                Id = entity,
                                Transform = transform.Value
                            },
                        
                            TargetEntity = request.Target
                        };
                    
                        coolDown.CurrentValue = coolDown.OriginValue;
                    }
                }
            }
        }
    }
}
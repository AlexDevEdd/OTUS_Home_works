using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class AttackingRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest, TransformView, TargetEntity, AttackCoolDown>, 
            Exc<FindTargetRequest>> _filter;
        
        private readonly EcsPoolInject<FindTargetRequest> _findTargetRequestPool;
        private readonly EcsPoolInject<AttackCoolDown> _attackCoolDownPool;
        private readonly EcsPoolInject<AttackRequest> _attackRequestPool;
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool;
        private readonly EcsPoolInject<AttackEvent> _attackEventPool;
        private readonly EcsPoolInject<Health> _healthPool;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            
            foreach (var entity in _filter.Value)
            {
                var request = _attackRequestPool.Value.Get(entity);
                ref var coolDown = ref _attackCoolDownPool.Value.Get(entity);

                if (_world.Value.IsEntityAlive(request.Target.Id))
                {
                    var targetHealth = _healthPool.Value.Get(request.Target.Id);

                    if (targetHealth.Value <= 0)
                    {
                        _targetEntityPool.Value.Del(entity);
                        _attackRequestPool.Value.Del(entity);
                        _findTargetRequestPool.Value.Add(entity) = new FindTargetRequest();
                    
                        coolDown.CurrentValue = coolDown.OriginValue;
                        continue;
                    } 
                    
                    coolDown.CurrentValue -= deltaTime;

                    if (targetHealth.Value > 0 && coolDown.CurrentValue <= 0)
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
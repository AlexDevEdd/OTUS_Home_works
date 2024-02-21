using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class DeSpawnRequestSystem : IEcsRunSystem
    {
        private const float FALL_DOWN_POS_Y = -1f;
        private const float FALL_DOWN_DURATION = 1f;
        
        private readonly EcsFilterInject<Inc<DeSpawnRequest, Inactive>> _filter;
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsPoolInject<SpawnSoulEvent> _eventPool;
        private readonly EcsCustomInject<UnitSystem> _unitSystem;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);
                _eventPool.Value.Add(entity) = new SpawnSoulEvent();
                DeSpawnProcess(entity);
            }
        }

        private void DeSpawnProcess(int id)
        {
            ref var transform = ref _transformViewPool.Value.Get(id);
            transform.Value.DOMoveY(FALL_DOWN_POS_Y, FALL_DOWN_DURATION)
               .OnComplete(() => DeSpawn(id));
        }

        private void DeSpawn(int id)
        {
            _unitSystem.Value.DeSpawn(id);
        }
    }
    
    internal sealed class AttackingRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackRequest, TransformView, TargetEntity, AttackCoolDown>, 
            Exc<FindTargetRequest>> _filter;
        
        private readonly EcsPoolInject<AttackRequest> _attackingRequestPool;
        private readonly EcsPoolInject<TransformView> _transformViewPool;

        private readonly EcsPoolInject<TargetEntity> _targetEntityPool;
        private readonly EcsPoolInject<AttackCoolDown> _attackCoolDownPool;
        private readonly EcsPoolInject<Health> _healthPool;
        
        private EcsCustomInject<EntityManager> _entityManager;

        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            
            foreach (var entity in _filter.Value)
            {
               
                var request = _attackingRequestPool.Value.Get(entity);
                ref var coolDown = ref _attackCoolDownPool.Value.Get(entity);
                var targetHealth = _healthPool.Value.Get(request.Target.Id);

                if (targetHealth.Value <= 0)
                {
                    _entityManager.Value.Get(entity).AddData(new FindTargetRequest());
                    coolDown.CurrentValue = coolDown.OriginValue;
                    continue;
                }

                coolDown.CurrentValue -= deltaTime;

                if (targetHealth.Value > 0 && coolDown.CurrentValue <= 0)
                {
                    var transform = _transformViewPool.Value.Get(entity);
                    _entityManager.Value.Get(entity).AddData(new AttackEvent
                    {
                        SourceEntity = new SourceEntity
                        {
                            Id = entity,
                            Transform = transform.Value
                        },
                        
                        TargetEntity = request.Target
                    });
                    
                    coolDown.CurrentValue = coolDown.OriginValue;
                }
            }
        }
    }
}
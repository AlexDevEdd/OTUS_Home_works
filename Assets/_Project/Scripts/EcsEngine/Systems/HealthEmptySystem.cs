using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class HealthEmptySystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Health>, Exc<DeathRequest, Inactive>> _filter;
        
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<DeathRequest> _deathPool;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var health = _healthPool.Value.Get(entity);
                if (health.Value <= 0)
                {
                    _targetPool.Value.Del(entity);
                    _deathPool.Value.Add(entity) = new DeathRequest();
                }
            }
        }
    }
}
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
        private EcsFilterInject<Inc<Health>, Exc<DeathRequest, Inactive>> _healthFilter;
        
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<DeathRequest> _deathPool;
        
        private EcsCustomInject<EntityManager> _entityManager;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _healthFilter.Value)
            {
                var health = _healthPool.Value.Get(entity);
                if (health.Value <= 0)
                {
                    _deathPool.Value.Add(entity) = new DeathRequest();
                }
            }
        }
    }
}
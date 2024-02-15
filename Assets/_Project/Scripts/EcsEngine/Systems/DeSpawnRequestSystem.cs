using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class DeSpawnRequestSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DeSpawnRequest, Inactive>> _filter;
        
        private EcsCustomInject<UnitSystem> _unitSystem;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);
                _unitSystem.Value.DeSpawn(entity);
            }
        }
    }
}
using _Project.Scripts.EcsEngine._OOP.Factories.Bullets;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class DeSpawnBulletLifeTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BulletTag, LifeTime>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<Inactive> _inactivePool;
        private readonly EcsPoolInject<LifeTime> _lifeTimePool;
        
        private readonly EcsCustomInject<BulletFactory> _bulletFactory;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var lifeTime = ref _lifeTimePool.Value.Get(entity);
                if (lifeTime.Value <= 0)
                {
                    _inactivePool.Value.Add(entity) = new Inactive();
                    _bulletFactory.Value.DeSpawn(entity);  
                }
            }
        }
    }
}
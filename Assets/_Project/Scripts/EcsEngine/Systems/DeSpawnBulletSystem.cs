using _Project.Scripts.EcsEngine._OOP.Factories.Bullets;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class DeSpawnBulletSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BulletTag, Collided>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<Inactive> _inactivePool;
        private readonly EcsPoolInject<Collided> _collidedPool;
        
        private readonly EcsCustomInject<BulletFactory> _bulletFactory;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _collidedPool.Value.Del(entity);
                _bulletFactory.Value.DeSpawn(entity);
            }
        }
    }
}
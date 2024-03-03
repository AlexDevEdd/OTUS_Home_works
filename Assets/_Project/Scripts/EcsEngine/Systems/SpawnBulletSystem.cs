using _Project.Scripts.EcsEngine._OOP.Factories.Bullets;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using _Project.Scripts.EcsEngine.Components.WeaponComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class SpawnBulletSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ProjectileWeapon, ShootEvent>, Exc<Inactive>> _filter;
       
        private readonly EcsPoolInject<ShootEvent> _shootEventPool;
        private readonly EcsPoolInject<ProjectileWeapon> _projectileWeaponPool;
        private readonly EcsPoolInject<Team> _teamPool;
        
        private readonly EcsCustomInject<BulletFactory> _bulletFactory;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var @event = _shootEventPool.Value.Get(entity);
                var team = _teamPool.Value.Get(@event.SourceId);
                var projectile = _projectileWeaponPool.Value.Get(entity);
                _bulletFactory.Value.Spawn(team.Value, projectile.FirePoint);
            }
        }
    }
}
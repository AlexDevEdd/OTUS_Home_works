using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using _Project.Scripts.EcsEngine.Components.WeaponComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class SpawnShootVfxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ProjectileWeapon, ShootEvent>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<ProjectileWeapon> _projectileWeaponPool;
        
        private readonly EcsCustomInject<VfxSystem> _vfxSystem;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var firePoint = _projectileWeaponPool.Value.Get(entity);
                _vfxSystem.Value.PlayFx(VfxType.Shoot, firePoint.FirePoint.position);
            }
        }
    }
}
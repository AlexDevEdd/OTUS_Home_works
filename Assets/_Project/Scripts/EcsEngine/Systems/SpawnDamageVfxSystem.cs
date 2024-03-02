using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine.Components.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class SpawnDamageVfxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnDamageTextEvent>> _filter = EcsWorlds.Events;
        
        private readonly EcsPoolInject<SpawnDamageTextEvent> _spawnDamageTextEventPool = EcsWorlds.Events;
        private readonly EcsWorldInject _eventWorld = EcsWorlds.Events;
        private readonly EcsCustomInject<VfxSystem> _vfxSystem;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var damageEvent = _spawnDamageTextEventPool.Value.Get(entity);
                var contactPos = damageEvent.ContactPosition;
                _vfxSystem.Value.PlayFx(VfxType.Damage, contactPos);
                
                contactPos.y += 4f;
                _vfxSystem.Value.PlayFx(VfxType.DamageText, contactPos, damageEvent.Damage);
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
    
}
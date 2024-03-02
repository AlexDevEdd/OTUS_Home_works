using _Project.Scripts.EcsEngine._OOP.Factories.Bullets;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class UnitRotationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Rotation, Position, TargetEntity, UnitTag>, Exc<Inactive, FindTargetRequest>> _filter;
        
        private readonly EcsPoolInject<Rotation> _rotationPool;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        
        public void Run(IEcsSystems systems)
        {
            var targetPool = _targetPool.Value;
            var rotationPool = _rotationPool.Value;
            var positionPool = _positionPool.Value;
            
            foreach (var entity in _filter.Value)
            {
                ref var rotation = ref rotationPool.Get(entity);
                var target = targetPool.Get(entity);

                if (target.Id == default)
                    continue;

                var direction = target.Transform.position - positionPool.Get(entity).Value;
                var targetRotation = Quaternion.LookRotation(direction);
                rotation.Value = targetRotation;
            }
        }
    }
    
    internal sealed class HitBoxCollisionRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionEnterRequest>> _filter = EcsWorlds.Events;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.Events;
        
        private readonly EcsPoolInject<Damage> _damagePool;
        private readonly EcsPoolInject<BulletTag> _bulletTagPool;
        private readonly EcsPoolInject<CollisionEnterRequest> _collisionEnterRequestPool = EcsWorlds.Events;
        //private readonly EcsPoolInject<DamageEvent> _damageEventPool;
        
        private readonly EcsCustomInject<BulletFactory> _bulletFactory;
        private readonly EcsCustomInject<VfxSystem> _vfxSystem;
        private readonly EcsCustomInject<EcsAdmin> _ecsAdmin;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var collisionEnterRequest = _collisionEnterRequestPool.Value.Get(entity);
                var damage = _damagePool.Value.Get(collisionEnterRequest.TargetId);
                
                _ecsAdmin.Value.CreateEntity(EcsWorlds.Events)
                .Add(new DamageEvent
                {
                    SourceId = collisionEnterRequest.SourceId,
                    TargetId = collisionEnterRequest.TargetId,
                    ContactPosition =collisionEnterRequest.ContactPosition,
                    Damage = damage.Value
                });
                
                _ecsAdmin.Value.CreateEntity(EcsWorlds.Events)
                .Add(new SpawnDamageTextEvent
                {
                    ContactPosition = collisionEnterRequest.ContactPosition,
                    Damage = damage.Value
                });
                
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
    
    internal sealed class DamageListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DamageEvent>> _filter = EcsWorlds.Events;
        
        private readonly EcsPoolInject<DamageEvent> _damageEventPool = EcsWorlds.Events;
        private readonly EcsPoolInject<Health> _healthPool;
        //private readonly EcsPoolInject<Collided> _collidedPool;
        //private readonly EcsPoolInject<BulletTag> _bulletTagPool;
        private readonly EcsWorldInject _eventWorld = EcsWorlds.Events;
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            
            foreach (var entity in _filter.Value)
            {
                var damageEvent = _damageEventPool.Value.Get(entity);
                // if (!_bulletTagPool.Value.Has(damageEvent.TargetEntity.Id))
                // {
                //     _collidedPool.Value.Del(damageEvent.TargetEntity.Id);
                // }
                
                ref var health = ref _healthPool.Value.Get(damageEvent.SourceId);
                health.Value -= damageEvent.Damage;
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}
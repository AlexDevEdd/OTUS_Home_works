using System;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Elements;
using Atomic.Objects;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Objects.Scripts;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    [Is(ObjectType.Damagable)]
    public sealed class HealthComponent : IDisposable
    {
        [SerializeField]
        private AtomicFunction<bool> _isAlive;
        
        [Get(ObjectAPI.HitPoints)]
        public AtomicVariable<int> HitPoints = new(10);
        public AtomicValue<float> DeSpawnDelay = new(3f);
        
        [Get(ObjectAPI.TakeDamageAction)]
        public AtomicEvent<int> TakeDamageEvent;
        
        [Get(ObjectAPI.TakeDamageAction)]
        public AtomicEvent DeathEvent;
        
        [Get(ObjectAPI.DeSpawnAction)]
        public AtomicEvent<IAtomicObject> DeSpawnEvent;
        
        [Get(ObjectAPI.IsAlive)]
        public IAtomicValue<bool> IsAlive => _isAlive;
        
        private TakeDamageMechanics _takeDamageMechanics;
        private DeathMechanics _deathMechanics;
        private DeSpawnMechanics _deSpawnMechanics;

        private int _cashedHitPoints;
        
        public void Compose(IAtomicObject atomicObject)
        {
            if(_cashedHitPoints == default)
                _cashedHitPoints = HitPoints.Value;

            HitPoints.Value = _cashedHitPoints;
            _isAlive.Compose(() => HitPoints.Value > 0);
            _takeDamageMechanics = new TakeDamageMechanics(TakeDamageEvent, HitPoints);
            _deathMechanics = new DeathMechanics(HitPoints, DeathEvent);
            _deSpawnMechanics = new DeSpawnMechanics(atomicObject, DeSpawnDelay, DeathEvent, DeSpawnEvent);
        }
        
        public void OnEnable()
        {
            _takeDamageMechanics?.OnEnable();
            _deathMechanics?.OnEnable();
            _deSpawnMechanics?.OnEnable();
        }

        public void OnDisable()
        {
            _takeDamageMechanics?.OnDisable();
            _deathMechanics?.OnDisable();
            _deathMechanics?.OnDisable();
            _deSpawnMechanics?.OnDisable();
        }

        public void Dispose()
        {
            DeathEvent?.Dispose();
            TakeDamageEvent?.Dispose();
            HitPoints?.Dispose();
            _deSpawnMechanics?.Dispose();
        }
    }
}
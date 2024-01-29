using System;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Elements;
using Atomic.Objects;
using Plugins.Atomic.Elements.Scripts.Interfaces;
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
        
        [Get(ObjectAPI.TakeDamageAction)]
        public AtomicEvent<int> TakeDamageEvent;
        
        public AtomicEvent DeathEvent;
        
        [Get(ObjectAPI.IsAlive)]
        public IAtomicValue<bool> IsAlive => _isAlive;
        
        private TakeDamageMechanics _takeDamageMechanics;
        private DeathMechanics _deathMechanics;

        public void Compose()
        {
            _isAlive.Compose(() => HitPoints.Value > 0);
            _takeDamageMechanics = new TakeDamageMechanics(TakeDamageEvent, HitPoints);
            _deathMechanics = new DeathMechanics(HitPoints, DeathEvent);
        }
        
        public void OnEnable()
        {
            _takeDamageMechanics.OnEnable();
            _deathMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _takeDamageMechanics.OnDisable();
            _deathMechanics.OnDisable();
        }

        public void Dispose()
        {
            DeathEvent?.Dispose();
            TakeDamageEvent?.Dispose();
            HitPoints?.Dispose();
        }
    }
}
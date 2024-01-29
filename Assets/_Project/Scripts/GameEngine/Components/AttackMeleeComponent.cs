using System;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Elements;
using Atomic.Objects;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    [Is(ObjectType.AttackMelee)]
    public sealed class AttackMeleeComponent : IDisposable, IUpdate
    {
        [SerializeField]
        private AtomicVariable<bool> _enabled = new(true);
        
        [SerializeField]
        private AtomicVariable<float> _attackCoolDown = new(1);
        
        [SerializeField]
        private AtomicVariable<int> _damage = new(1);
        
        [SerializeField]
        private AtomicEvent _attackEvent;
        
        [SerializeField] 
        private AtomicEvent _targetDieEvent;
        
        public IAtomicVariable<bool> Enabled => _enabled;
        public IAtomicVariable<float> AttackCoolDown => _attackCoolDown;
        public IAtomicVariable<int> Damage => _damage;
        
        public IAtomicObservable AttackEvent => _attackEvent; 
        public IAtomicEvent TargetDieEvent => _targetDieEvent;

        private AttackMeleeMechanic _attackMeleeMechanic;

        public AttackMeleeComponent(IAtomicValue<bool> isAlive, IAtomicObservable<bool> isReached)
        {
            Compose(isAlive, isReached);
        }
        
        public void Compose(IAtomicValue<bool> isAlive, IAtomicObservable<bool> isReached)
        {
            _enabled.Value = isAlive.Value;

            _attackMeleeMechanic = new AttackMeleeMechanic(_attackCoolDown, _attackEvent, isReached, _targetDieEvent);
        }
        
        public void OnEnable()
        {
            _attackMeleeMechanic.OnEnable();
        }

        public void OnDisable()
        {
           _attackMeleeMechanic.OnDisable();
        }
        
        public void Update()
        {
            if (_enabled.Value)
            {
               
            }
        }

        public void Dispose()
        {
            _attackEvent?.Dispose();
            _enabled?.Dispose(); 
        }
    }
}
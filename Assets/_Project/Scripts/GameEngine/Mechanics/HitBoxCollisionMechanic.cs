using _Project.Scripts.GameEngine.Interfaces;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class HitBoxCollisionMechanic
    {
        private readonly IAtomicVariable<int> _damage;
        private readonly IAtomicEvent _onTargetDieEvent;
        private readonly IAtomicEvent _collisionEvent;
        
        public HitBoxCollisionMechanic(IAtomicVariable<int> damage, IAtomicEvent onTargetDieEvent,
            IAtomicEvent collisionEvent)
        {
            _damage = damage;
            _onTargetDieEvent = onTargetDieEvent;
            _collisionEvent = collisionEvent;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damageObj))
            {
                _collisionEvent?.Invoke();
                damageObj?.OnTakeDamage?.Invoke(_damage.Value);
                if (damageObj != null && !damageObj.IsAlive.Value)
                {
                    _onTargetDieEvent?.Invoke();
                }
            }
        }
    }
}
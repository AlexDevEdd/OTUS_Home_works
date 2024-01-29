using _Project.Scripts.GameEngine.Interfaces;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class HitBoxCollisionMechanic
    {
        private readonly IAtomicVariable<int> _damage;
        private readonly IAtomicEvent _onTargetDieEvent;
        
        public HitBoxCollisionMechanic(IAtomicVariable<int> damage, IAtomicEvent onTargetDieEvent)
        {
            _damage = damage;
            _onTargetDieEvent = onTargetDieEvent;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damageObj))
            {
                damageObj?.OnTakeDamage.Invoke(_damage.Value);
                if (damageObj != null && !damageObj.IsAlive.Value)
                {
                    _onTargetDieEvent.Invoke();
                }
            }
        }
    }
}
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class EnableColliderMechanic
    {
        private readonly IAtomicObservable _onEnableEvent;
        private readonly Collider _collider;
        
        public EnableColliderMechanic(Collider collider, IAtomicObservable onEnable)
        {
            _onEnableEvent = onEnable;
            _collider = collider;
        }

        public void OnEnable()
        {
            _onEnableEvent.Subscribe(OnEnableCollider);
        }
        
        public void OnDisable()
        {
            _onEnableEvent.Unsubscribe(OnEnableCollider);
        }
        
        private void OnEnableCollider()
        {
            _collider.enabled = true;
        }
    }
}
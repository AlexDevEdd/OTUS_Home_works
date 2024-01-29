using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class DisableColliderMechanic
    {
        private readonly IAtomicObservable _onDisableEvent;
        private readonly Collider _collider;
        
        public DisableColliderMechanic(Collider collider, IAtomicObservable onDisable)
        {
            _onDisableEvent = onDisable;
            _collider = collider;
        }

        public void OnEnable()
        {
            _onDisableEvent.Subscribe(OnDisableCollider);
        }
        
        public void OnDisable()
        {
            _onDisableEvent.Unsubscribe(OnDisableCollider);
        }
        
        private void OnDisableCollider()
        {
            _collider.enabled = false;
        }
    }
}
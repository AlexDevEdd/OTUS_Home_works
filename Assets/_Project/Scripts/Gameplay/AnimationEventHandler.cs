using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public sealed class AnimationEventHandler : MonoBehaviour
    {
        [SerializeField]
        private AtomicEvent _onEnableCollider;
        
        [SerializeField]
        private AtomicEvent _onDisableCollider;

        public IAtomicObservable OnEnableColliderEvent => _onEnableCollider;
        public IAtomicObservable OnDisableColliderEvent => _onDisableCollider;
        
        public void OnEnableCollider()
        {
            _onEnableCollider.Invoke();
        }
        
        public void OnDisableCollider()
        {
            _onDisableCollider.Invoke(); 
        }
    }
}
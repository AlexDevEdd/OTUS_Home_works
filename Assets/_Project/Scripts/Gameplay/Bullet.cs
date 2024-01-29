using _Project.Scripts.GameEngine.Components;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Behaviours;
using Atomic.Elements;
using Plugins.Atomic.Objects.Scripts.Attributes;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public sealed class Bullet : AtomicBehaviour
    {
        [SerializeField]
        private bool composeOnAwake = true;
        
        [SerializeField]
        private AtomicVariable<int> _damage = new(1);
        
        [SerializeField]
        private AtomicEvent _targetDieEvent;
        
        [Section]
        public MoveComponent _moveComponent;
        
        private HitBoxCollisionMechanic _hitBoxCollisionMechanic;
        
        public override void Compose()
        {
            base.Compose();
            _moveComponent.Compose(transform);
            _hitBoxCollisionMechanic = new HitBoxCollisionMechanic(_damage, _targetDieEvent);
        }

        private void Awake()
        {
            if (composeOnAwake)
            {
                Compose();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _hitBoxCollisionMechanic.OnTriggerEnter(other);
        }

        protected override void Update()
        {
            base.Update();
            _moveComponent.Update();
        }

        private void OnDestroy()
        {
            _moveComponent.Dispose();
        }
    }
}
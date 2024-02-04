using _Project.Scripts.GameEngine.Components;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Elements;
using Plugins.Atomic.Behaviours.Scripts;
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
        private AtomicVariable<float> _lifeTime = new(5);
        
        [SerializeField]
        private AtomicEvent _targetDieEvent;
        
        [SerializeField]
        private AtomicEvent _collisionEvent;
        
        [Section]
        public MoveComponent _moveComponent;
        
        private HitBoxCollisionMechanic _hitBoxCollisionMechanic;
        private DestroyMechanic _destroyMechanic;
        
        public override void Compose()
        {
            base.Compose();
            _moveComponent.Compose(transform);
            _hitBoxCollisionMechanic = new HitBoxCollisionMechanic(_damage, _targetDieEvent, _collisionEvent);
            _destroyMechanic = new DestroyMechanic(gameObject, _collisionEvent, _lifeTime);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _destroyMechanic?.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _destroyMechanic?.OnDisable();
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
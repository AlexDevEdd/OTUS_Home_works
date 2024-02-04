using _Project.Scripts.GameEngine.Mechanics;
using _Project.Scripts.Gameplay.Units;
using Atomic.Elements;
using Plugins.Atomic.Behaviours.Scripts;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class Fist: AtomicBehaviour
    {
        [SerializeField]
        private bool composeOnAwake = true;
        
        [SerializeField] 
        private Zombie _zombie;

        [SerializeField] 
        private AnimationEventHandler _animationEventHandler;

        [SerializeField]
        private AtomicEvent _collisionEvent;
        
        [SerializeField]
        private Collider _collider;
        
        private HitBoxCollisionMechanic _hitBoxCollisionMechanic;
        private EnableColliderMechanic _enableColliderMechanic;
        private DisableColliderMechanic _disableColliderMechanic;
        
        public override void Compose()
        {
            base.Compose();
            
            _enableColliderMechanic = new EnableColliderMechanic(_collider, _animationEventHandler.OnEnableColliderEvent);
            _disableColliderMechanic = new DisableColliderMechanic(_collider, _animationEventHandler.OnDisableColliderEvent);
            _hitBoxCollisionMechanic = new HitBoxCollisionMechanic(_zombie._core.AttackMeleeComponent.Damage,
                _zombie._core.AttackMeleeComponent.TargetDieEvent, _collisionEvent);
        }

        private void Awake()
        {
            if (composeOnAwake)
            {
                Compose();
            }
        }

        protected override void OnEnable()
        {
            _enableColliderMechanic.OnEnable();
            _disableColliderMechanic.OnEnable();
        }

        protected override void OnDisable()
        {
            _enableColliderMechanic.OnDisable();
            _disableColliderMechanic.OnDisable();
        }

        private void OnTriggerEnter(Collider other)
        {
            _hitBoxCollisionMechanic.OnTriggerEnter(other);
        }
    }
}
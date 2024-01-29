using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class CheckDistanceMechanic
    {
        private readonly IAtomicVariable<float> _attackDistance;
        private readonly IAtomicVariable<bool> _isReached;
        private readonly IAtomicValue<bool> _isAlive;

        private readonly Transform _entity;
        private readonly Transform _target;

        public CheckDistanceMechanic(Transform entity, Transform target, IAtomicVariable<float> attackDistance,
            IAtomicValue<bool> isAlive, IAtomicVariable<bool> isReached)
        {
            _attackDistance = attackDistance;
            _isAlive = isAlive;
            _isReached = isReached;
            _entity = entity;
            _target = target;
        }
        
        public void Update()
        {
            if (_isAlive.Value && Vector3.Distance(_entity.position, _target.position) < _attackDistance.Value)
            {
                _isReached.Value = true;
                return;
            }
            
            _isReached.Value = false;
        }
    }
    
    public sealed class LifeTimeMechanic
    {
        private float lifetime;
        private readonly AtomicEvent onDeath;

        public LifeTimeMechanic(AtomicVariable<float> lifeTime, AtomicEvent onDeath)
        {
            lifetime = lifeTime.Value;
            this.onDeath = onDeath;
        }

        public void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
            {
                onDeath?.Invoke();
            }
        }
    }
    
    public sealed class DeleteObjectMechanic
    {
        private readonly AtomicEvent deleteMe;
        private readonly Transform targetTransform;

        public DeleteObjectMechanic(AtomicEvent deleteMe, Transform targetTransform)
        {
            this.deleteMe = deleteMe;
            this.targetTransform = targetTransform;
        }

        private void DestroyObject()
        {
            Object.Destroy(targetTransform.gameObject);
        }

        public void OnEnable()
        {
            deleteMe.Subscribe(DestroyObject);
        }

        public void OnDisable()
        {
           // deleteMe.UnSubscribe(DestroyObject);
        }
    }
}
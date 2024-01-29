using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class RotationToTargetMechanics : IUpdate
    {
        private readonly Transform _entity;
        private readonly Transform _target;
        
        private readonly IAtomicValue<bool> _enabled;
        private readonly IAtomicValue<bool> _isAlive;
        private readonly IAtomicVariable<Vector3> _lookDirection;

        public RotationToTargetMechanics(Transform entity, Transform target, IAtomicValue<bool> enabled,
            IAtomicVariable<Vector3> lookDirection, IAtomicValue<bool> isAlive)
        {
            _entity = entity;
            _target = target;
            _enabled = enabled;
            _lookDirection = lookDirection;
            _isAlive = isAlive;
        }

        public void Update()
        {
            if (!_enabled.Value || !_isAlive.Value)
                return;

            var direction = _target.position;
            if (direction == Vector3.zero)
                return;

            _entity.LookAt(new Vector3(direction.x, _entity.position.y, direction.z));
            _lookDirection.Value = _entity.forward;
        }
    }
}
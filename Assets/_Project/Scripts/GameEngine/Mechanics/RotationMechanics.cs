using Atomic.Elements;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class RotationMechanics : IUpdate
    {
        private readonly IAtomicValue<bool> _enabled;
        private readonly IAtomicValue<Vector3> _lookDirection;
        private readonly Transform _transform;

        public RotationMechanics(IAtomicValue<bool> enabled, IAtomicValue<Vector3> lookDirection, Transform transform)
        {
            _enabled = enabled;
            _lookDirection = lookDirection;
            _transform = transform;
        }
        
        public void Update()
        {
            if (!_enabled.Value)
            {
                return;
            }

            Vector3 direction = _lookDirection.Value;
            if (direction == Vector3.zero)
            {
                return;
            }
            
            _transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
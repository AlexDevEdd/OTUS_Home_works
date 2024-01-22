using Atomic.Behaviours;
using Atomic.Elements;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class MouseRotationMechanics : IUpdate
    {
        private readonly IAtomicValue<bool> _enabled;
        private readonly IAtomicVariable<float> _speed;
        private readonly IAtomicValue<Vector3> _lookDirection;
        private readonly Transform _transform;
        
        public MouseRotationMechanics(IAtomicValue<bool> enabled, IAtomicValue<Vector3> lookDirection,
            IAtomicVariable<float> rotateSpeed, Transform transform)
        {
            _enabled = enabled;
            _lookDirection = lookDirection;
            _speed = rotateSpeed;
            _transform = transform;
        }
        
        public void OnUpdate()
        {
            if(!_enabled.Value)
                return;
            
            var bodyPosition = _transform.position;
            var worldPos = new Vector3(_lookDirection.Value.x, bodyPosition.y, _lookDirection.Value.z);
            var direction = worldPos - bodyPosition;
            var targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, Time.deltaTime * _speed.Value);
        }
    }
}
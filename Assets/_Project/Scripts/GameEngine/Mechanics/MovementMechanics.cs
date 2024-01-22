using Atomic.Elements;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class MovementMechanics
    {
        private readonly IAtomicValue<bool> _moveEnabled;
        private readonly IAtomicValue<Vector3> _moveDirection;
        private readonly IAtomicValue<float> _moveSpeed;
        private readonly Transform _transform;

        public MovementMechanics(
            IAtomicValue<bool> moveEnabled,
            IAtomicValue<Vector3> moveDirection,
            IAtomicValue<float> moveSpeed,
            Transform transform
        )
        {
            _moveEnabled = moveEnabled;
            _moveDirection = moveDirection;
            _moveSpeed = moveSpeed;
            _transform = transform;
        }

        public void Update()
        {
            if (_moveEnabled.Value)
            {
                var moveOffset = _moveDirection.Value * (_moveSpeed.Value * Time.deltaTime);
                _transform.position += moveOffset;
            }
        }
    }
}
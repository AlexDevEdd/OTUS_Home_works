using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class TargetMovementMechanic
    {
        private readonly Transform _entity;
        private readonly IAtomicVariable<Vector3> _moveDirection;
        private readonly IAtomicValue<bool> _isAlive;
        private readonly IAtomicVariable<float> _moveSpeed;
        private readonly AtomicVariable<bool> _isReached;

        public TargetMovementMechanic(Transform entity, IAtomicVariable<Vector3> moveDirection,
            IAtomicValue<bool> isAlive, IAtomicVariable<float> moveSpeed, AtomicVariable<bool> isReached)
        {
            _moveDirection = moveDirection;
            _entity = entity;
            _isAlive = isAlive;
            _moveSpeed = moveSpeed;
            _isReached = isReached;
        }

        public void Update()
        {
            if (_isAlive.Value && !_isReached.Value)
            {
                var moveOffset = _moveDirection.Value * (_moveSpeed.Value * Time.deltaTime);
                _entity.position += moveOffset;
            }
        }
    }
}
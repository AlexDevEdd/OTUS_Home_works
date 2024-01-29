using System;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Elements;
using Atomic.Objects;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    [Is(ObjectType.Movable)]
    public sealed class MoveComponent : IDisposable, IUpdate
    {
        public IAtomicValue<bool> IsMoving => _isMoving;

        public IAtomicVariable<bool> Enabled => _enabled;

        public IAtomicVariable<float> Speed => _speed;

        [Get("MoveCondition")]
        public IAtomicExpression<bool> Condition => _moveCondition;

        [Get(ObjectAPI.MoveDirection)]
        public IAtomicVariable<Vector3> Direction => _direction;

        [SerializeField]
        private AtomicVariable<float> _speed = new();

        [SerializeField]
        private AtomicVariable<bool> _enabled = new(true);

        [SerializeField]
        private AtomicVariable<Vector3> _direction = new();

        [SerializeField]
        private AtomicFunction<bool> _isMoving = new();

        [SerializeField]
        private AndExpression _moveCondition = new();

        private MovementMechanics _movementMechanics;

        public MoveComponent(Transform transform, float speed = default)
        {
            Compose(transform);
            _speed.Value = speed;
        }
        
        public void Compose(Transform transform)
        {
            _isMoving.Compose(
                () => _enabled.Value && _direction.Value.magnitude > 0 && _moveCondition.Invoke()
            );

            _movementMechanics = new MovementMechanics(
                _moveCondition, _direction, _speed, transform
            );
        }

        public void Update()
        {
            if (_enabled.Value)
            {
                _movementMechanics.Update();
            }
        }

        public void Dispose()
        {
            _enabled?.Dispose();
            _speed?.Dispose();
            _direction?.Dispose();
        }
    }
}
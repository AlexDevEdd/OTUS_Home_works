using System;
using _Project.Scripts.GameEngine.Functions;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Elements;
using Atomic.Objects;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    [Is(ObjectType.TargetMovable)]
    public sealed class TargetMoveComponent : IDisposable, IUpdate
    {
        [SerializeField]
        private AtomicVariable<float> _speed = new();
        
        [SerializeField]
        private AtomicVariable<float> _attackDistance = new();

        [SerializeField]
        private AtomicVariable<bool> _isReached = new(false);
        
        [SerializeField]
        private AtomicVariable<bool> _enabled = new(true);

        [SerializeField]
        private AtomicVariable<Vector3> _direction = new();

        [SerializeField]
        private AtomicFunction<bool> _isMoving = new();

        [SerializeField]
        private AndExpression _moveCondition = new();
        public IAtomicValue<bool> IsMoving => _isMoving;
        public IAtomicObservable<bool> IsReached => _isReached;

        public IAtomicVariable<bool> Enabled => _enabled;

        public IAtomicVariable<float> Speed => _speed;

        [Get(ObjectAPI.MoveCondition)]
        public IAtomicExpression<bool> Condition => _moveCondition;

        [Get(ObjectAPI.MoveDirection)]
        public IAtomicVariable<Vector3> Direction => _direction;
        
        private TargetMovementMechanic _movementMechanics;
        private RotationToTargetMechanics _rotationMechanics;
        private CheckDistanceMechanic _checkDistanceMechanic;
        
        public TargetMoveComponent(Transform entity, Transform target, IAtomicValue<bool> isAlive, float speed = default)
        {
            Compose(entity, target, isAlive);
            _speed.Value = speed;
        }
        
        public void Compose(Transform entity, Transform target, IAtomicValue<bool> isAlive)
        {
            _isMoving.Compose(()
                => _enabled.Value && !_isReached.Value);
            
            _rotationMechanics = new RotationToTargetMechanics(entity, target, _enabled, _direction, isAlive);
            _movementMechanics = new TargetMovementMechanic(entity, _direction, isAlive, _speed, _isReached);
            _checkDistanceMechanic = new CheckDistanceMechanic(entity, target, _attackDistance,
                isAlive, _isReached);
        }

        public void Update()
        {
            if (_enabled.Value)
            {
                _rotationMechanics?.Update();
                _checkDistanceMechanic?.Update();
                _movementMechanics?.Update();
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
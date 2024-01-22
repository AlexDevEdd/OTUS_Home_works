using System;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    [Is(ObjectType.MouseRotate)]
    public sealed class MouseRotateComponent : IDisposable, IUpdate
    {
        public IAtomicVariable<bool> Enabled => _enabled;

        public IAtomicVariable<float> RotationSpeed => rotationSpeed;

        // [Get("MoveCondition")]
        // public IAtomicExpression<bool> Condition => _moveCondition;

        [Get(ObjectAPI.RotateDirection)]
        public IAtomicVariable<Vector3> Direction => _direction;

        [SerializeField]
        private AtomicVariable<float> rotationSpeed = new();

        [SerializeField]
        private AtomicVariable<bool> _enabled = new(true);

        [SerializeField]
        private AtomicVariable<Vector3> _direction = new();
        
        // [SerializeField]
        // private AndExpression _moveCondition = new();

        private  MouseRotationMechanics _mouseRotationMechanics;

        public MouseRotateComponent(Transform transform, float speed = default)
        {
            Compose(transform);
            rotationSpeed.Value = speed;
        }

        public MouseRotateComponent()
        {
        }

        public void Compose(Transform transform)
        {
            // this._isMoving.Compose(
            //     () => _enabled.Value && _direction.Value.magnitude > 0 && _moveCondition.Invoke()
            // );

            _mouseRotationMechanics = new MouseRotationMechanics(
                _enabled, _direction, rotationSpeed, transform
            );
        }

        public void OnUpdate()
        {
            if (_enabled.Value)
            {
                _mouseRotationMechanics.OnUpdate();
            }
        }

        public void Dispose()
        {
            _enabled?.Dispose();
            rotationSpeed?.Dispose();
            _direction?.Dispose();
        }
    }
}
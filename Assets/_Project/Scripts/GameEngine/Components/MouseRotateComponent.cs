using System;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using Plugins.Atomic.Behaviours.Scripts;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    [Is(ObjectType.MouseRotate)]
    public sealed class MouseRotateComponent : IDisposable, IUpdate
    {
        public IAtomicVariable<bool> Enabled => _enabled;

        public IAtomicVariable<float> RotationSpeed => _rotationSpeed;
        
        [Get(ObjectAPI.RotateDirection)]
        public IAtomicVariable<Vector3> Direction => _direction;

        [SerializeField]
        private AtomicVariable<float> _rotationSpeed = new();

        [SerializeField]
        private AtomicVariable<bool> _enabled = new(true);

        [SerializeField]
        private AtomicVariable<Vector3> _direction = new();
        
        private  MouseRotationMechanics _mouseRotationMechanics;

        public MouseRotateComponent(Transform transform, IAtomicValue<bool> IsAlive, float speed = default)
        {
            Compose(transform, IsAlive);
            _rotationSpeed.Value = speed;
        }
        
        public void Compose(Transform transform, IAtomicValue<bool> IsAlive)
        {
            _mouseRotationMechanics = new MouseRotationMechanics(
                _enabled, _direction, _rotationSpeed, transform, IsAlive
            );
        }

        public void Update()
        {
            if (_enabled.Value)
            {
                _mouseRotationMechanics.Update();
            }
        }

        public void Dispose()
        {
            _enabled?.Dispose();
            _rotationSpeed?.Dispose();
            _direction?.Dispose();
        }
    }
}
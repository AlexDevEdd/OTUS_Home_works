using System;
using _Project.Scripts.GameEngine.Components;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Character
{
    [Serializable]
    public sealed class Character_Core : IDisposable
    {
        [Get(ObjectAPI.Transform)]
        [SerializeField] private Transform _transform;
        
        [Section] 
        public HealthComponent _healthComponent;
        
        [Section] 
        public FireComponent _fireComponent;
        
        [Section]
        public MoveComponent MoveComponent;
        
        [Section] 
        public MouseRotateComponent MouseRotateComponent;
        
        private UpdateMechanics _stateController;

        public void Compose()
        {
            _healthComponent.Compose();
            _fireComponent.Compose();
            MoveComponent.Compose(_transform);
            MouseRotateComponent.Compose(_transform);
            
            _stateController = new UpdateMechanics(() =>
            {
                bool isAlive = _healthComponent.IsAlive.Value;
                _fireComponent.enabled.Value = isAlive;
            });
        }

        public void OnEnable()
        {
            _healthComponent.OnEnable();
        }

        public void OnDisable()
        {
            _healthComponent.OnDisable();
        }

        public void Update()
        {
            _stateController.OnUpdate();
            MoveComponent.OnUpdate();
            MouseRotateComponent.OnUpdate();
        }

        public void Dispose()
        {
            _healthComponent?.Dispose();
            _fireComponent?.Dispose();
        }
    }
}
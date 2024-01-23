using System;
using _Project.Scripts.GameEngine.Components;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Objects;
using GameEngine;
using Plugins.Atomic.Objects.Scripts.Attributes;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Character
{
    [Serializable]
    public sealed class Character_Core : IDisposable
    {
        [Get(ObjectAPI.Transform)]
        [SerializeField] private Transform _transform;
        
        [Section] 
        public HealthComponent HealthComponent;
        
        [Section] 
        public FireComponent FireComponent;
        
        [Section] 
        public RegenerationComponent RegenerationComponent;
        
        [Section]
        public MoveComponent MoveComponent;
        
        [Section] 
        public MouseRotateComponent MouseRotateComponent;
        
        private UpdateMechanics _stateController;

        public void Compose()
        {
            HealthComponent.Compose();
            FireComponent.Compose();
            MoveComponent.Compose(_transform);
            MouseRotateComponent.Compose(_transform);
            RegenerationComponent.Compose(FireComponent.Charges, FireComponent.MaxCharges);
            
            _stateController = new UpdateMechanics(() =>
            {
                bool isAlive = HealthComponent.IsAlive.Value;
                FireComponent.Enabled.Value = isAlive;
            });
        }

        public void OnEnable()
        {
            HealthComponent.OnEnable();
            RegenerationComponent.OnEnable();
        }

        public void OnDisable()
        {
            HealthComponent.OnDisable();
            RegenerationComponent.OnDisable();
        }

        public void Update()
        {
            _stateController.OnUpdate();
            MoveComponent.OnUpdate();
            MouseRotateComponent.OnUpdate();
        }

        public void Dispose()
        {
            HealthComponent?.Dispose();
            FireComponent?.Dispose();
        }
    }
}
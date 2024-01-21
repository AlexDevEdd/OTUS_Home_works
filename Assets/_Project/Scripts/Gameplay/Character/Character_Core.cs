using System;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class Character_Core : IDisposable
    {
        [Get(ObjectAPI.Transform)]
        [SerializeField]
        private Transform transform;
        
        [Section]
        public HealthComponent healthComponent;

        [Section]
        public FireComponent fireComponent;

        [Section]
        public MoveComponent MoveComponent;

        [Section]
        public RotationMechanics RotationMechanics;
        
        private UpdateMechanics stateController;

        public void Compose()
        {
            this.healthComponent.Compose();
            this.fireComponent.Compose();
            MoveComponent.Compose(transform);
            RotationMechanics = new RotationMechanics(
               healthComponent.IsAlive,
               MoveComponent.Direction,
               transform
            );
            
            this.stateController = new UpdateMechanics(() =>
            {
                bool isAlive = this.healthComponent.IsAlive.Value;
                this.fireComponent.enabled.Value = isAlive;
            });
        }

        public void OnEnable()
        {
            this.healthComponent.OnEnable();
        }

        public void OnDisable()
        {
            this.healthComponent.OnDisable();
        }

        public void Update()
        {
            this.stateController.OnUpdate();
            MoveComponent.OnUpdate();
            RotationMechanics.OnUpdate();
        }

        public void Dispose()
        {
            this.healthComponent?.Dispose();
            this.fireComponent?.Dispose();
        }
    }
}
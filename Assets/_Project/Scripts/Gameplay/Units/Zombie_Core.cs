using System;
using _Project.Scripts.GameEngine;
using _Project.Scripts.GameEngine.Components;
using Atomic.Objects;
using Plugins.Atomic.Objects.Scripts;
using Plugins.Atomic.Objects.Scripts.Attributes;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    [Serializable]
    public sealed class Zombie_Core : IDisposable
    {
        [Get(ObjectAPI.Transform)]
        [SerializeField] private Transform _transform;
        
        [Section] 
        public HealthComponent HealthComponent;
        
        [Section]
        public TargetMoveComponent TargetMoveComponent;

        [Section] 
        public AttackMeleeComponent AttackMeleeComponent;
        
        public void Compose(Transform target,IAtomicObject atomicObject)
        {
            HealthComponent.Compose(atomicObject);
            TargetMoveComponent.Compose(_transform, target, HealthComponent.IsAlive);
            AttackMeleeComponent.Compose(HealthComponent.IsAlive,TargetMoveComponent.IsReached);
        }

        public void OnEnable()
        {
            HealthComponent?.OnEnable();
            AttackMeleeComponent?.OnEnable();
        }

        public void OnDisable()
        {
            HealthComponent?.OnDisable();
            AttackMeleeComponent?.OnDisable();
        }
        
        public void Update()
        {
            TargetMoveComponent?.Update();
        }

        public void Dispose()
        {
            HealthComponent?.Dispose();
            TargetMoveComponent?.Dispose();
        }
    }
}
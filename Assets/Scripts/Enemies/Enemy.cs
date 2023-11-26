using System;
using Bullets;
using Components;
using UnityEngine;

namespace Enemies
{
    public sealed class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDied;
        
        [SerializeField] private EnemyAttackController _enemyAttackController;
        [SerializeField] private EnemyMoveController _moveController;
        [SerializeField] private HealthComponent _healthComponent;
        
        public void Construct(BulletSystem bulletSystem) 
            => _enemyAttackController.Construct(bulletSystem);

        public void Enable()
        {
            _healthComponent.OnDied += OnDiedEvent;
            _moveController.OnDestinationReached += OnShootEvent;
        }

        public void Disable()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            _moveController.OnDestinationReached -= OnShootEvent;
            _enemyAttackController.Disable();
        }

        private void OnShootEvent()
        {
             _moveController.OnDestinationReached -= OnShootEvent;
            _enemyAttackController.StartLoopFire();
        } 

        private void OnDiedEvent()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            OnDied?.Invoke(this);
        }

        public void SetPosition(Vector2 position) 
            => transform.position = position; 
        
        public void SetAttackPosition(Vector2 position) 
            => _moveController.SetDestination(position);

        public void SetAttackTarget(Vector2 target) 
            => _enemyAttackController.SetTarget(target);

        public void SetHealth(float health) 
            => _healthComponent.SetHealth(health);

        public void UpdatePhysics(float fixedDeltaTime) 
            => _moveController.UpdatePhysics(fixedDeltaTime);
    }
}
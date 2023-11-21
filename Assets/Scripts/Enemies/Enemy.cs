using System;
using Bullets;
using Components;
using UnityEngine;

namespace Enemies
{
    public sealed class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDied;
        
        [SerializeField] private EnemyAttackController enemyAttackController;
        [SerializeField] private EnemyMoveController _moveController;
        [SerializeField] private HealthComponent _healthComponent;
        
        public void Construct(BulletSystem bulletSystem) 
            => enemyAttackController.Construct(bulletSystem);

        private void OnEnable()
        {
            _healthComponent.OnDied += OnDiedEvent;
            _moveController.OnDestinationReached += OnShootEvent;
        }

        private void OnDisable()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            _moveController.OnDestinationReached -= OnShootEvent;
        }
        
        private void OnShootEvent()
        {
            _moveController.OnDestinationReached -= OnShootEvent;
            enemyAttackController.StartLoopFire();
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
            => enemyAttackController.SetTarget(target);

        public void SetHealth(float health) 
            => _healthComponent.SetHealth(health);

        public void UpdatePhysics(float fixedDeltaTime) 
            => _moveController.UpdatePhysics(fixedDeltaTime);
        
    }
}
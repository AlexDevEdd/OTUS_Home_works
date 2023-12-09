using System;
using Bullets;
using Components;
using UnityEngine;

namespace Enemies
{
    public sealed class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDied;

        [SerializeField] private EnemyController _enemyController;
        [SerializeField] private HealthComponent _healthComponent;
        
        public void Construct(BulletSystem bulletSystem) 
            => _enemyController.Construct(bulletSystem);

        public void Enable()
        {
            _healthComponent.OnDied += OnDiedEvent;
            _enemyController.Enable();
        }

        public void Disable()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            _enemyController.Disable();
        }
        
        private void OnDiedEvent()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            OnDied?.Invoke(this);
        }

        public void SetPosition(Vector2 position) 
            => transform.position = position; 
        
        public void SetAttackPosition(Vector2 position) 
            => _enemyController.SetDestination(position);

        public void SetAttackTarget(Vector2 target) 
            => _enemyController.SetAttackTarget(target);

        public void SetHealth(float health) 
            => _healthComponent.SetHealth(health);

        public void UpdatePhysics(float fixedDeltaTime) 
            => _enemyController.UpdatePhysics(fixedDeltaTime);
    }
}
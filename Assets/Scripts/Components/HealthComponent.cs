using System;
using Bullets;
using Common.Interfaces;
using UnityEngine;

namespace Components
{
    public sealed class HealthComponent : MonoBehaviour, IDamageable
    {
        public event Action OnDied;
        
        private float _health;

        public void SetHealth(float health)
            => _health = health;
        
        public void ApplyDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                OnDied?.Invoke();
            }
        }
    }
}
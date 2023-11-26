using System;
using Bullets;
using Character;
using Common.Interfaces;
using UnityEngine;

namespace Components
{
    public sealed class HealthComponent : MonoBehaviour, IDamageable
    {
        public event Action OnDied;
        
        [SerializeField] private TeamType _teamType;
        
        private float _health;
        
        public bool IsPlayer => _teamType == TeamType.Player;
        
        public void SetHealth(float health)
            => _health = health;


        public void ApplyDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0) 
                OnDied?.Invoke();
        }
    }
}
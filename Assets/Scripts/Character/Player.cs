using System;
using Components;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(HealthComponent))]
    public sealed class Player : MonoBehaviour
    {
        public event Action OnDied;
        
        [SerializeField] private float _health = 10;
        
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.SetHealth(_health);
            _healthComponent.OnDied += OnDiedEvent;
        }

        private void OnDiedEvent()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            OnDied?.Invoke();
        }

        private void OnDisable() 
            => _healthComponent.OnDied -= OnDiedEvent;
    }
}
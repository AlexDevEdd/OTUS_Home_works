using System;
using Common.Interfaces;
using Components;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(HealthComponent))]
    public sealed class Player : MonoBehaviour , IGameStart, IGameFinish
    {
        public event Action OnDied;
        
        [SerializeField] private float _health = 10;
        
        private HealthComponent _healthComponent;
        
        public void OnStart()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.SetHealth(_health);
            _healthComponent.OnDied += OnDiedEvent;
        }

        public void OnFinish()
            => _healthComponent.OnDied -= OnDiedEvent;
        
        private void OnDiedEvent()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            OnDied?.Invoke();
        }
    }
}
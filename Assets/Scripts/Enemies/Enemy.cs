using System;
using Components;
using UnityEngine;

namespace Enemies
{
    public sealed class Enemy : MonoBehaviour
    {
        public event Action<Enemy> OnDied;
        public event Action<Transform> OnFire;
        
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private MoveComponent _moveComponent;

        private void OnEnable()
        {
            _healthComponent.OnDied += OnDiedEvent;
            _weaponComponent.OnFire += OnFireEvent;
            _moveComponent.OnTargetReached += OnDiedEvent;
        }

        private void OnFireEvent(Transform startPoint) 
            => OnFire?.Invoke(startPoint);

        private void OnDisable()
        { 
            _healthComponent.OnDied -= OnDiedEvent;
            _weaponComponent.OnFire -= OnFireEvent;
            _moveComponent.OnTargetReached -= OnDiedEvent;
        }

        private void OnDiedEvent()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            OnDied?.Invoke(this);
        }

        public void SetPosition(Vector2 position) 
            => transform.position = position;
        
        public void SetTarget(Transform target) 
            => _moveComponent.SetDestination(target.position);

        public void SetSpeed(float speed) 
            => _moveComponent.SetSpeed(speed);

        public void SetHealth(float health) 
            => _healthComponent.SetHealth(health);

        public void UpdatePhysics(float fixedDeltaTime) 
            => _moveComponent.UpdatePhysics(fixedDeltaTime);
    }
}
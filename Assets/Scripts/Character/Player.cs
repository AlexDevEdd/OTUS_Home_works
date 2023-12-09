using System;
using Bullets;
using Common.Interfaces;
using Components;
using Systems.InputSystem;
using UnityEngine;
using Zenject;

namespace Character
{
    [RequireComponent(typeof(HealthComponent))]
    public sealed class Player : MonoBehaviour , IGameStart, IGameFinish, IFixedTick
    {
        public event Action OnDied;
        
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _health = 10;
        
        private HealthComponent _healthComponent;
        private MoveComponent _moveComponent;
        
        private PlayerAttackController _attackController;
        private PlayerMoveController _moveController;

        [Inject]
        public void Construct(IInput input, BulletSystem bulletSystem)
        {
            _healthComponent = GetComponent<HealthComponent>();
            _moveComponent = GetComponent<MoveComponent>();

            _moveController = new PlayerMoveController(input, _moveComponent);
            _attackController = new PlayerAttackController(input, bulletSystem, _firePoint);
        }
        
        public void OnStart()
        {
            _healthComponent.SetHealth(_health);
            _healthComponent.OnDied += OnDiedEvent;
            _attackController.OnStart();
            _moveController.OnStart();
        }

        public void OnFinish()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            _attackController.OnFinish();
            _moveController.OnFinish();
        }
        
        private void OnDiedEvent()
        {
            _healthComponent.OnDied -= OnDiedEvent;
            OnDied?.Invoke();
        }

        public void FixedTick(float fixedDelta)
        {
            _moveController.FixedTick(fixedDelta);
        }
    }
}
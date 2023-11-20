﻿using Bullets;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private Transform _firePoint;

        private void OnEnable() 
            => _inputListener.OnFire += OnFireEvent;

        private void OnFireEvent() 
            => _bulletSystem.Fire(BulletType.Player, _firePoint.position, _firePoint.up);

        private void OnDisable() 
            => _inputListener.OnFire -= OnFireEvent;
    }
}
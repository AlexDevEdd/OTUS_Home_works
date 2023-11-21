﻿using Bullets;
using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _coolDown;
        
        private BulletSystem _bulletSystem;
        
        private Tween _tween;
        private Vector2 _targetPos;

        public void Construct(BulletSystem bulletSystem) 
            => _bulletSystem = bulletSystem;
        
        private void OnDisable() 
            => _tween?.Kill();

        public void SetTarget(Vector2 target) 
            => _targetPos = target;
        
        public void StartLoopFire()
        {
            Fire();
            _tween = DOVirtual.DelayedCall(_coolDown, Fire)
                .SetLoops(-1);
        }

        private void Fire()
        {
            CalculateDirection(out var direction);
            _bulletSystem.Fire(BulletType.Enemy, _firePoint.position, direction);
        } 

        private void CalculateDirection(out Vector2 direction)
        {
            var vector = _targetPos - (Vector2)transform.position;
            direction = vector.normalized;
        }
    }
}
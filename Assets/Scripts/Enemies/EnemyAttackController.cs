using Bullets;
using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemyAttackController
    {
        private const TeamType TEAM_TYPE = TeamType.Enemy;
        private const float COOL_DOWN = 1.5f;
        
        private readonly Transform _firePoint;
        private readonly Transform _selfTransform;
        
        private BulletSystem _bulletSystem;

        public EnemyAttackController(BulletSystem bulletSystem, Transform firePoint, Transform self)
        {
            _bulletSystem = bulletSystem;
            _firePoint = firePoint;
            _selfTransform = self;
        }

        private Tween _tween;
        private Vector2 _targetPos;
        
        public void Construct(BulletSystem bulletSystem) 
            => _bulletSystem = bulletSystem;
        
        
        public void SetTarget(Vector2 target) 
            => _targetPos = target;
        
        public void Disable()
            => _tween?.Kill();
        
        public void StartLoopFire()
        {
            Fire();
            _tween = DOVirtual.DelayedCall(COOL_DOWN, Fire)
                .SetLoops(-1);
        }

        private void Fire()
        {
            CalculateDirection(out var direction);
            _bulletSystem.Fire(TEAM_TYPE, _firePoint.position, direction);
        } 

        private void CalculateDirection(out Vector2 direction)
        {
            var vector = _targetPos - (Vector2)_selfTransform.position;
            direction = vector.normalized;
        }
    }
}
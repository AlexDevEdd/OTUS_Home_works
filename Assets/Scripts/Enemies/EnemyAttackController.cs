using Bullets;
using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private BulletConfig _config;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _coolDown;
        
        private BulletSystem _bulletSystem;
        
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
            _tween = DOVirtual.DelayedCall(_coolDown, Fire)
                .SetLoops(-1);
        }

        private void Fire()
        {
            CalculateDirection(out var direction);
            _bulletSystem.Fire(_config, _firePoint.position, direction);
        } 

        private void CalculateDirection(out Vector2 direction)
        {
            var vector = _targetPos - (Vector2)transform.position;
            direction = vector.normalized;
        }
    }
}
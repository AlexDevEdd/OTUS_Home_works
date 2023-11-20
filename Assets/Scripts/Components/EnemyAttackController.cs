using Bullets;
using DG.Tweening;
using Enemies;
using UnityEngine;

namespace Components
{
    public sealed class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private EnemyMoveController _moveController;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _coolDown;
        
        private BulletSystem _bulletSystem;
        
        private Tween _tween;
        private Vector2 _targetPos;

        public void Construct(BulletSystem bulletSystem) 
            => _bulletSystem = bulletSystem;
        
        private void OnEnable() 
            => _moveController.OnDestinationReached += OnShootEvent;

        private void OnDisable()
        {
            _moveController.OnDestinationReached -= OnShootEvent;
            _tween?.Kill();
        } 
        
        public void SetTarget(Vector2 target) 
            => _targetPos = target;

        private void OnShootEvent()
        {
            _moveController.OnDestinationReached -= OnShootEvent;
            StartLoopFire();
        }

        private void StartLoopFire()
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
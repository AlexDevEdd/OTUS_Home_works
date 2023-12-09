using Bullets;
using Components;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemyController
    { 
        private readonly EnemyAttackController _enemyAttackController;
        private readonly EnemyMoveController _moveController;

       public EnemyController(BulletSystem bulletSystem, MoveComponent moveComponent,
           Transform firePoint, Transform self)
       {
           _enemyAttackController = new EnemyAttackController(bulletSystem, firePoint, self);
           _moveController = new EnemyMoveController(moveComponent, self);
       }
       
        public void Enable()
        {
            _moveController.OnDestinationReached += OnShootEvent;
        }

        public void Disable()
        {
            _moveController.OnDestinationReached -= OnShootEvent;
            _enemyAttackController.Disable();
        }

        private void OnShootEvent()
        {
            _moveController.OnDestinationReached -= OnShootEvent;
            _enemyAttackController.StartLoopFire();
        }

        public void SetAttackTarget(Vector2 target)
            => _enemyAttackController.SetTarget(target);

        public void SetDestination(Vector2 position) 
            => _moveController.SetDestination(position);

        public void UpdatePhysics(float fixedDeltaTime) 
            => _moveController.UpdatePhysics(fixedDeltaTime);
    }
}
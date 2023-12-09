using Bullets;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyAttackController enemyAttackController;
        [SerializeField] private EnemyMoveController moveController;
        
        public void Construct(BulletSystem bulletSystem) 
            => enemyAttackController.Construct(bulletSystem);
        
        public void Enable()
        {
            moveController.OnDestinationReached += OnShootEvent;
        }

        public void Disable()
        {
            moveController.OnDestinationReached -= OnShootEvent;
            enemyAttackController.Disable();
        }

        private void OnShootEvent()
        {
            moveController.OnDestinationReached -= OnShootEvent;
            enemyAttackController.StartLoopFire();
        }

        public void SetAttackTarget(Vector2 target)
            => enemyAttackController.SetTarget(target);

        public void SetDestination(Vector2 position) 
            => moveController.SetDestination(position);

        public void UpdatePhysics(float fixedDeltaTime) 
            => moveController.UpdatePhysics(fixedDeltaTime);
    }
}
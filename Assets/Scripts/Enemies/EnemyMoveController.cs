using System;
using Components;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemyMoveController : MonoBehaviour
    {
        public event Action OnDestinationReached;
        
        [SerializeField] private MoveComponent _moveComponent;
        
        private Vector2 _destination;
        private bool _isReached;
        
        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }
        
        public void UpdatePhysics(float fixedDeltaTime) 
        {
            if (_isReached) return;

            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.025f)
            {
                _isReached = true;
                OnDestinationReached?.Invoke();
                return;
            }

            var direction = vector.normalized * fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}
using System;
using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        public event Action OnTargetReached;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private float _speed;
        private Vector2 _destination;
        private bool _isReached;
        
        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }
        
        public void SetSpeed(float speed) 
            => _speed = speed;
        
        public void UpdatePhysics(float fixedDeltaTime)
        {
            if (!_isReached && !CalculateDistance(out var vector)) 
                MoveByRigidbodyVelocity(vector, fixedDeltaTime);
        }
        
        private bool CalculateDistance(out Vector2 vector)
        {
            vector = _destination - (Vector2)transform.position;
            
            if (vector.magnitude <= Constants.EnemyStopDistance)
            {
                _isReached = true;
                OnTargetReached?.Invoke();
                return true;
            }

            return false;
        }

        private void MoveByRigidbodyVelocity(Vector2 vector, float fixedDeltaTime)
        {
            var nextPosition = _rigidbody2D.position + vector * (_speed * fixedDeltaTime);
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}
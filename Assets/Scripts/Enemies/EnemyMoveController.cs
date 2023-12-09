using System;
using Components;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemyMoveController
    {
        private const float DISTANCE_THRESHOLD = 0.025f;
        public event Action OnDestinationReached;
        
        private readonly MoveComponent _moveComponent;
        private readonly Transform _selfTransform;
        
        private Vector2 _destination;
        private bool _isReached;
        public EnemyMoveController(MoveComponent moveComponent, Transform self)
        {
            _moveComponent = moveComponent;
            _selfTransform = self;
        }
        
        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }
        
        public void UpdatePhysics(float fixedDeltaTime) 
        {
            if (_isReached) return;

            var vector = _destination - (Vector2)_selfTransform.position;
            if (vector.sqrMagnitude <= DISTANCE_THRESHOLD * DISTANCE_THRESHOLD)
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
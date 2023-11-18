using Input;
using UnityEngine;

namespace Components
{
    public sealed class PlayerMoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private InputListener _input;
        [SerializeField] private float _speed = 5.0f;

        private float _horizontalDirection;

        private void OnEnable() 
            => _input.OnDirectionChanged += OnDirectionChanged;

        private void OnDisable()
            => _input.OnDirectionChanged -= OnDirectionChanged;

        private void OnDirectionChanged(float direction) 
            => _horizontalDirection = direction;

        private void FixedUpdate() 
            => MoveByRigidbodyVelocity(new Vector2(_horizontalDirection, Constants.YDirection) * Time.fixedDeltaTime);

        private void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}
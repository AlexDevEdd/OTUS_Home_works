using Components;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private MoveComponent _moveComponent;

        private float _horizontalDirection;

        private void OnEnable() 
            => _inputListener.OnDirectionChanged += OnDirectionChanged;

        private void OnDisable()
            => _inputListener.OnDirectionChanged -= OnDirectionChanged;

        private void OnDirectionChanged(float direction) 
            => _horizontalDirection = direction;
        
        private void FixedUpdate()
            => _moveComponent.MoveByRigidbodyVelocity(
                new Vector2(_horizontalDirection, Constants.YDirection) * Time.fixedDeltaTime);
    }
}
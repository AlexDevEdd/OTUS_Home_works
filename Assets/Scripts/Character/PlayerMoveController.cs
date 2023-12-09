using Components;
using Systems.InputSystem;
using UnityEngine;

namespace Character
{
    public sealed class PlayerMoveController
    {
        private readonly IInputMove _input;
        private readonly MoveComponent _moveComponent;

        private float _horizontalDirection;

        public PlayerMoveController(IInputMove input, MoveComponent moveComponent)
        {
            _input = input;
            _moveComponent = moveComponent;
        }
        
        public void OnStart()
            => _input.OnMoveEvent += OnDirectionChanged;

        public void OnFinish()
            => _input.OnMoveEvent -= OnDirectionChanged;
        
        private void OnDirectionChanged(Vector2 direction) 
            => _horizontalDirection = direction.x;
        
        public void FixedTick(float fixedDelta)
        {
            _moveComponent.MoveByRigidbodyVelocity(
                new Vector2(_horizontalDirection, Constants.YDirection) * fixedDelta);
        }
    }
}
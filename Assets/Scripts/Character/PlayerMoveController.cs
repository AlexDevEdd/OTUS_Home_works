using Common.Interfaces;
using Components;
using Input;
using UnityEngine;


namespace Character
{
    public sealed class PlayerMoveController : MonoBehaviour, IFixedTick, IGameStart, IGameFinish
    {
        [SerializeField] private InputListener _input;
        [SerializeField] private MoveComponent _moveComponent;

        private float _horizontalDirection;
        
        public void OnStart() 
            => _input.OnDirectionChanged += OnDirectionChanged;

        public void OnFinish()
            => _input.OnDirectionChanged -= OnDirectionChanged;
        
        public void FixedTick(float fixedDelta)
        {
            _moveComponent.MoveByRigidbodyVelocity(
                new Vector2(_horizontalDirection, Constants.YDirection) * fixedDelta);
        }

        private void OnDirectionChanged(float direction) 
            => _horizontalDirection = direction;
       
    }
}
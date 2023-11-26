using Common.Interfaces;
using Components;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class PlayerMoveController : MonoBehaviour, IFixedTick, IGameStart, IGamePause, IGameResume, IGameFinish
    {
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private MoveComponent _moveComponent;

        private float _horizontalDirection;
        private bool _isPaused;
        
        private void OnDirectionChanged(float direction) 
            => _horizontalDirection = direction;
        
        public void FixedTick(float fixedDelta)
        {
            if(_isPaused) return;
            
            _moveComponent.MoveByRigidbodyVelocity(
                new Vector2(_horizontalDirection, Constants.YDirection) * fixedDelta);
        }

        public void OnStart()
        {
            _inputListener.OnDirectionChanged += OnDirectionChanged;
        }

        private void SetIsPaused(bool isPaused)
            => _isPaused = isPaused;

        public void OnPause() 
            => SetIsPaused(true);

        public void OnResume() 
            => SetIsPaused(false);

        public void OnFinish()
            => _inputListener.OnDirectionChanged -= OnDirectionChanged;
    }
}
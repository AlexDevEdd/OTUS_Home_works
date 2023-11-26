using Common.Interfaces;
using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour, IGameStart, IGamePause, IGameResume, IFixedTick
    {
        [SerializeField] private Params _params;

        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;

        private Transform _cachedTransform;
        private bool _isPaused;

        public void OnStart()
        {
            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;
            _cachedTransform = transform;
            var position = _cachedTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }
        
        public void OnPause() 
            => SetIsPaused(true);

        public void OnResume() 
            => SetIsPaused(false);

        public void FixedTick(float fixedDelta)
        {
            if(_isPaused) return;
            
            if (_cachedTransform.position.y <= _endPositionY)
            {
                _cachedTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _cachedTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * fixedDelta,
                _positionZ
            );
        }
        
        private void SetIsPaused(bool isPaused)
            => _isPaused = isPaused;
    }
}
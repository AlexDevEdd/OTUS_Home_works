using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField] private Params _params;

        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;

        private Transform _cachedTransform;

        private void Awake()
        {
            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;
            _cachedTransform = transform;
            var position = _cachedTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        private void FixedUpdate()
        {
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
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }
    }
}
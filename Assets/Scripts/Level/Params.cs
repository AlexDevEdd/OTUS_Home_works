using System;
using UnityEngine;

namespace Level
{
    [Serializable]
    public sealed class Params
    {
        [SerializeField] private float _startPositionY;
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;

        public float StartPositionY => _startPositionY;
        public float EndPositionY => _endPositionY;
        public float MovingSpeedY => _movingSpeedY;
    }
}
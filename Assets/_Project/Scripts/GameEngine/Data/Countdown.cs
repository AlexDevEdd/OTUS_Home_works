using System;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Data
{
    [Serializable]
    public sealed class Countdown
    {
        public float _duration;
        public float _currentTime;

        public Countdown()
        {
        }

        public Countdown(float duration)
        {
            _duration = duration;
        }

        public bool IsPlaying()
        {
            return _currentTime > 0;
        }

        public bool IsEnded()
        {
            return _currentTime <= 0;
        }

        public void Tick(float deltaTime)
        {
            _currentTime = Mathf.Max(_currentTime - deltaTime, 0);
        }

        public void Reset()
        {
            _currentTime = _duration;
        }
    }
}
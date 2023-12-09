using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Tools.Timers
{
    public class TestTimer : MonoBehaviour
    {
        [SerializeField] private float _endValue;
        [SerializeField] private float _intervalValue;
        [SerializeField] private float _step;
        
        [Space]
        [SerializeField] private float _startValue_2;
        [SerializeField] private float _intervalValue_2;
        
        private ITimer _timer;
        private ITimer _reversTimer;

        private int _currentValue;
        private void OnEnable()
        {
            _timer = new Timer( _intervalValue, _step, _endValue);
            _reversTimer = new ReversTimer(_startValue_2, _intervalValue_2, _step);
            
            _currentValue = 0;
        }

        [Button]
        public void StartTimer()
        {
            _timer.OnTimerUpdated += OnTimerUpdated;
            _timer.OnTimerEnd += OnTimerEnd;
            _timer.StartTimer();
        }
        
        [Button]
        public void StartReversTimer()
        {
            _reversTimer.OnTimerUpdated += OnTimerUpdated;
            _reversTimer.OnTimerEnd += OnTimerEnd;
            _reversTimer.StartTimer(); 
        }
        
        private void OnDisable()
        {
            _timer.OnTimerUpdated -= OnTimerUpdated;
            _timer.OnTimerEnd -= OnTimerEnd;
            
            _reversTimer.OnTimerUpdated -= OnTimerUpdated;
            _reversTimer.OnTimerEnd -= OnTimerEnd;

            _currentValue = 0;
        }

        private void OnTimerEnd()
        {
            Debug.LogError($"OnTimerUpdated {_currentValue}");
        }

        private void OnTimerUpdated(float obj)
        {
            _currentValue = (int)obj;
            Debug.LogWarning($"OnTimerUpdated {_currentValue}");
        }
    }
}
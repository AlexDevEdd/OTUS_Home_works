using System;
using System.Globalization;
using Common.Tools.Timers;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class StartGameTimer : MonoBehaviour
    {
        public event Action OnStartTimerCompleted;

        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private float _startValue = 3;
        [SerializeField] private float _intervalValue = 1;
        [SerializeField] private float _stepValue = 1;
        
        private ITimer _timer;
        
        private void Awake()
        {
            _timer = new ReversTimer(_startValue, _intervalValue, _stepValue);
            _timer.OnTimerUpdated += OnTimerUpdated;
            _timer.OnTimerEnd += OnTimerEnd;
        }
        
        public void StartTimer() 
            => _timer.StartTimer();

        private void OnTimerEnd()
        {
            _timer.OnTimerUpdated -= OnTimerUpdated;
            _timer.OnTimerEnd -= OnTimerEnd;
            
            Deactivate();
            OnStartTimerCompleted?.Invoke();
        }

        private void OnTimerUpdated(float newValue) 
            => SetText(newValue);

        private void SetText(float value) 
            => _timerText.text = value.ToString(CultureInfo.CurrentCulture);
        
        private void Deactivate()
             => gameObject.SetActive(false);
    }
}
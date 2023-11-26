using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class StartGameTimer : MonoBehaviour
    {
        private const float TIMER_DELAY = 1f;
        
        public event Action OnStartGame;

        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private int _startValue = 3;

        private Tween _tween;
        
        public void StartTimer()
        {
            var timerValue = _startValue;
            SetText(timerValue.ToString());
            _tween = DOVirtual.DelayedCall(TIMER_DELAY, ()=>
            {
                if (timerValue == 0)
                {
                    _tween.Kill();
                    Deactivate();
                    OnStartGame?.Invoke();
                    
                    return;
                }

                timerValue--;
                SetText(timerValue.ToString());
                
            }).SetLoops(-1);
          
        }

        private void SetText(string value) 
            => _timerText.text = value;
        
        private void Deactivate()
             => gameObject.SetActive(false);
    }
}
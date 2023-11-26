using System;
using UnityEngine;

namespace Input
{
    public sealed class InputListener : MonoBehaviour
    {
        public event Action OnFire;
        public event Action OnPause;
        public event Action OnResume;
        public event Action<float> OnDirectionChanged;

        private bool _isPause;
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isPause)
                {
                    _isPause = false;
                    OnResume?.Invoke();
                }
                else
                {
                    _isPause = true;
                    OnPause?.Invoke();
                }
            }
            if(_isPause) return;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                OnFire?.Invoke();

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                OnDirectionChanged?.Invoke(Constants.XDirectionLeft);
            
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                OnDirectionChanged?.Invoke(Constants.XDirectionRight);
            else
                OnDirectionChanged?.Invoke(Constants.XDirectionDefault);
            
            
        }
    }
}
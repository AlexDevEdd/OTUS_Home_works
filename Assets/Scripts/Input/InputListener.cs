using System;
using UnityEngine;

namespace Input
{
    public sealed class InputListener : MonoBehaviour
    {
        public event Action OnFire; 
        public event Action<float> OnDirectionChanged; 
        
        private void Update()
        {
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
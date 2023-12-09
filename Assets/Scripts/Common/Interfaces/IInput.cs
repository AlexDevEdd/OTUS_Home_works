using System;

namespace Common.Interfaces
{
    public interface IInput
    {
        public event Action OnFire;
        public event Action OnPause;
        public event Action OnResume;
        public event Action<float> OnDirectionChanged;
        
    }
}
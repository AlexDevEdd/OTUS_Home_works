using System;

namespace Common.Tools.Timers
{
    public interface ITimer
    {
        public event Action<float> OnTimerUpdated;
        public event Action OnTimerEnd;
        public void StartTimer();
    }
}
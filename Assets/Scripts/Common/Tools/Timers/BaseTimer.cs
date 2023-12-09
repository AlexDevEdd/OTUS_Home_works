using System;

namespace Common.Tools.Timers
{
    public abstract class BaseTimer : ITimer, IDisposable
    {
        public event Action<float> OnTimerUpdated;
        public event Action OnTimerEnd;

        protected IDisposable TimerDisposable;
        protected float IntervalSeconds { get; }
        protected float EndValue { get; set; }
        protected float CurrentValue { get; set; }
        protected float Step { get; }
        
        public abstract void StartTimer();

        public abstract void Dispose();

        protected BaseTimer(float intervalSeconds, float step)
        {
            IntervalSeconds = intervalSeconds;
            Step = step;
        }

        protected void InvokeTimerUpdate(float newValue)
            => OnTimerUpdated?.Invoke(newValue);

        protected void InvokeTimerEnd() 
            => OnTimerEnd?.Invoke();
    }
}
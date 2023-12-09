using System;
using UniRx;

namespace Common.Tools.Timers
{
    public sealed class ReversTimer : BaseTimer
    {
        public ReversTimer(float startValue, float intervalSeconds, float step)
            : base(intervalSeconds, step)
        {
            CurrentValue = startValue;
        }
        
        public override void StartTimer()
        {
            InvokeTimerUpdate(CurrentValue);
            TimerDisposable = Observable.Interval(TimeSpan.FromSeconds(IntervalSeconds))
                .Subscribe(time => UpdateTimer(Step));
        }

        private void UpdateTimer(float value)
        {
            CurrentValue -= value;
            InvokeTimerUpdate(CurrentValue);

            if ((int)CurrentValue == (int)EndValue)
            {
                InvokeTimerEnd();
                Dispose();
            }
        }

        public override void Dispose() 
            => TimerDisposable?.Dispose();
    }
}
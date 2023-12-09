using System;
using UniRx;

namespace Common.Tools.Timers
{
    public sealed class Timer : BaseTimer
    {
        private const float START_VALUE = 0;
        public Timer(float intervalSeconds, float step, float endValue)
            : base(intervalSeconds, step)
        {
            CurrentValue = START_VALUE;
            EndValue = endValue;
        }
        
        public override void StartTimer()
        {
            UpdateTimer(CurrentValue);
            TimerDisposable =  Observable.Interval(TimeSpan.FromSeconds(IntervalSeconds))
                .TakeWhile(time => time <= EndValue)
                .Subscribe(time => UpdateTimer(Step));
        }

        private void UpdateTimer(float newValue)
        {
            CurrentValue += newValue;
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
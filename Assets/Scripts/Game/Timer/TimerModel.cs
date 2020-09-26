using System;
using UniRx;

namespace Game.Timer
{
    public class TimerModel
    {
        private ReactiveProperty<int> timerValue;

        public IReadOnlyReactiveProperty<int> TimerValue => timerValue;

        private bool _isActiveTimer = true;
        
        public TimerModel()
        {
            timerValue = new ReactiveProperty<int>(0);
        }

        public void SetTimeValue(int timeValue)
        {
            timerValue.Value = timeValue;
        }
        
        public IObservable<long> CreateTimerObservable()
        {
            return Observable
                .Timer(TimeSpan.Zero,TimeSpan.FromSeconds(1))
                .TakeWhile(x => _isActiveTimer);
        }

        public void ActivateTimer(bool isActive)
        {
            _isActiveTimer = isActive;
        }
    }
}

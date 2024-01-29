using System;
using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using UniRx;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public class AttackMeleeMechanic : IDisposable
    {
        private const float ATTACK_DURATION = 1.5f;
        
        private readonly IAtomicVariable<float> _attackCoolDown;
        private readonly IAtomicObservable<bool> _isReached;
        private readonly IAtomicObservable _atomicEvent;
        private readonly IAtomicAction _attackEvent;
        
        private IDisposable _disposable;

        public AttackMeleeMechanic(IAtomicVariable<float> attackCoolDown, IAtomicAction attackEvent,
            IAtomicObservable<bool> isReached, IAtomicObservable atomicEvent)
        {
            _attackCoolDown = attackCoolDown;
            _attackEvent = attackEvent;
            _isReached = isReached;
            _atomicEvent = atomicEvent;
        }

        public void OnEnable()
        {
            _isReached.Subscribe(OnAttack);
            _atomicEvent.Subscribe(Dispose);
        }
        
        public void OnDisable()
        {
            _isReached.Unsubscribe(OnAttack);
            _atomicEvent.Unsubscribe(Dispose);
            Dispose();
        }
        
        private void OnAttack(bool flag)
        {
            if (flag)
            {
                AttackCallback();
                _disposable = Observable.Interval(TimeSpan.FromSeconds(ATTACK_DURATION + _attackCoolDown.Value))
                    .Subscribe(_ => AttackCallback());
            }
            else
            {
                Dispose();
            }
        }

        private void AttackCallback()
        {
            _attackEvent.Invoke();
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
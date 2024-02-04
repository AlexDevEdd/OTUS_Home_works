using System;
using System.Threading;
using Atomic.Elements;
using Cysharp.Threading.Tasks;
using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public class CycleDelayCallbackMechanics
    {
        private readonly IAtomicVariable<bool> _enabled;
        private readonly AtomicVariable<float> _delay;
        private readonly IAtomicEvent _callbackEvent;

        private CancellationTokenSource _token;
        
        public CycleDelayCallbackMechanics(IAtomicVariable<bool> enabled, AtomicVariable<float> delay, IAtomicEvent callbackEvent)
        {
            _enabled = enabled;
            _delay = delay;
            _callbackEvent = callbackEvent;
        }

        public void OnEnable()
        {
            _delay?.Subscribe(OnIntervalChanged);
            StartSpawn().Forget();
        }
        
        private void OnIntervalChanged(float value)
        {
            _token?.Cancel();
            _token?.Dispose();
            StartSpawn().Forget();
        }

        private async UniTaskVoid StartSpawn()
        {
            _token = new CancellationTokenSource();
            
            while (_enabled.Value)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delay.Value), cancellationToken: _token.Token);
                _callbackEvent?.Invoke();
            }
        }
        
        public void Dispose()
        {
            _delay?.Unsubscribe(OnIntervalChanged);
            _token?.Cancel();
            _token?.Dispose();
        }
    }
}
using System;
using System.Threading;
using Atomic.Elements;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public class RegenerationMechanics
    {
        private readonly IAtomicVariable<bool> _enabled;
        private readonly IAtomicVariable<int> _charges;
        private readonly IAtomicVariable<int> _increaseValue;
        private readonly IAtomicVariable<float> _delay;
        private readonly IAtomicValue<int> _maxCharges;

        private CancellationTokenSource _token;
        public RegenerationMechanics(IAtomicVariable<bool> enabled, IAtomicVariable<int> charges, 
            IAtomicValue<int> maxCharges, IAtomicVariable<float> delay, IAtomicVariable<int> increaseValue)
        {
            _enabled = enabled;
            _charges = charges;
            _delay = delay;
            _increaseValue = increaseValue;
            _maxCharges = maxCharges;
        }

        public async UniTaskVoid StartRegeneration()
        {
            _token = new CancellationTokenSource();
            
            while (_enabled.Value)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delay.Value), cancellationToken: _token.Token);
                if (_charges.Value < _maxCharges.Value)
                {
                    _charges.Value += _increaseValue.Value;
                    //reloadAmo?.Invoke();    
                }
            }
        }
        
        public void StopRegeneration()
        {
            _token?.Cancel();
            _token?.Dispose();
        }
    }
}
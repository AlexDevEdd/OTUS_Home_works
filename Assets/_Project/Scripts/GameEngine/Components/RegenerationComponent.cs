using System;
using _Project.Scripts.GameEngine.Mechanics;
using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace _Project.Scripts.GameEngine.Components
{
    [Serializable]
    public sealed class RegenerationComponent : IDisposable
    {
        public AtomicVariable<bool> _enabled = new(true);
        public AtomicVariable<float> _regenerationDelay = new(2);
        public AtomicVariable<int> _increaseValue = new(1);
        public AtomicEvent _increaseEvent;
        
        private RegenerationMechanics _regenerationMechanics;
        
        public void Compose(IAtomicVariable<int> charges, IAtomicValue<int> maxCharges)
        {
            _regenerationMechanics = new RegenerationMechanics(_enabled, charges, maxCharges,
                _regenerationDelay, _increaseValue);
        }
        
        public void OnEnable()
        {
            _regenerationMechanics.StartRegeneration().Forget();
        }
        
        public void OnDisable()
        {
            _regenerationMechanics.StopRegeneration();
        }
        
        public void Dispose()
        {
            _increaseEvent?.Dispose();
        }
    }
}
using System;
using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;

namespace _Project.Scripts.GameEngine.Functions
{
    [Serializable]
    public sealed class FireCondition : IAtomicFunction<bool>
    {
        private IAtomicValue<bool> _enabled;
        private IAtomicValue<int> _charges;

        public void Compose(IAtomicValue<bool> enabled, IAtomicValue<int> charges)
        {
            _enabled = enabled;
            _charges = charges;
        }

        public bool Invoke()
        {
            return _enabled.Value && _charges.Value > 0;
        }
    }
}
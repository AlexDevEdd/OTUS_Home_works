using System;
using Atomic.Elements;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Sirenix.OdinInspector;

namespace _Project.Scripts.GameEngine.Actions
{
    [Serializable]
    public sealed class FireAction : IAtomicAction
    {
        private IAtomicVariable<int> _charges;
        private IAtomicValue<bool> _shootCondition;
        private IAtomicAction _shootAction;
        private IAtomicEvent _shootEvent;

        public void Compose(
            IAtomicAction shootAction,
            IAtomicVariable<int> charges,
            IAtomicValue<bool> shootCondition,
            IAtomicEvent shootEvent
        )
        {
            _shootAction = shootAction;
            _charges = charges;
            _shootCondition = shootCondition;
            _shootEvent = shootEvent;
        }

        [Button]
        public void Invoke()
        {
            if (!_shootCondition.Value)
            {
                return;
            }

            _shootAction.Invoke();
            _charges.Value--;
            _shootEvent.Invoke();
        }
    }
}
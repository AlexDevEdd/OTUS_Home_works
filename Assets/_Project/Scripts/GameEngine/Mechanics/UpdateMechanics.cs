using System;
using Plugins.Atomic.Behaviours.Scripts;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class UpdateMechanics : IUpdate
    {
        private readonly Action _action;

        public UpdateMechanics(Action action)
        {
            _action = action;
        }

        public void Update()
        {
            _action?.Invoke();
        }
    }
}
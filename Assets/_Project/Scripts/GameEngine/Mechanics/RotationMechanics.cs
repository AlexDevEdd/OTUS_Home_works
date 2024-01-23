using Atomic.Behaviours;
using Atomic.Elements;
using Plugins.Atomic.Behaviours.Scripts;
using UnityEngine;

namespace _Project.Scripts.GameEngine.Mechanics
{
    public sealed class RotationMechanics : IUpdate
    {
        private readonly IAtomicValue<bool> enabled;
        private readonly IAtomicValue<Vector3> lookDirection;
        private readonly Transform transform;

        public RotationMechanics(IAtomicValue<bool> enabled, IAtomicValue<Vector3> lookDirection, Transform transform)
        {
            this.enabled = enabled;
            this.lookDirection = lookDirection;
            this.transform = transform;
        }
        
        public void OnUpdate()
        {
            if (!this.enabled.Value)
            {
                return;
            }

            Vector3 direction = this.lookDirection.Value;
            if (direction == Vector3.zero)
            {
                return;
            }
            
            this.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
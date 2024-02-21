using System;

namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct AttackRequest
    {
        public TargetEntity Target;
    }
}
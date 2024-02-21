using System;

namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct AttackEvent
    {
        public SourceEntity SourceEntity;
        public TargetEntity TargetEntity;
    }
}
using System;

namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct AttackEvent
    {
        public int SourceEntity;
        public int TargetEntity;
    }
}
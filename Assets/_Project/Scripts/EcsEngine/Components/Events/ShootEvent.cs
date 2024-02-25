using System;

namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct ShootEvent
    {
        public SourceEntity SourceEntity;
        public TargetEntity TargetEntity;
    }
}
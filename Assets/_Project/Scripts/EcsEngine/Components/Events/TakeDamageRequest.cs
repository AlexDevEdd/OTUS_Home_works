using System;

namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct TakeDamageRequest
    {
        public SourceEntity SourceEntity;
        public TargetEntity TargetEntity;
        public Position ContactPosition;
    }
}
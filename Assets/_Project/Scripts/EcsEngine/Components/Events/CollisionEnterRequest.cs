using System;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct CollisionEnterRequest
    {
        public int SourceId;
        public int TargetId;
        public Vector3 ContactPosition;
    }
}
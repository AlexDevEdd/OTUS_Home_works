using System;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct DamageEvent
    {
        public int SourceId;
        public int TargetId;
        public Vector3 ContactPosition;
        public float Damage;
    }
}
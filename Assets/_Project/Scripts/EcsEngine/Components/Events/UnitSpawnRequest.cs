using System;
using _Project.Scripts.EcsEngine.Enums;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct UnitSpawnRequest
    {
        public UnitType UnitType;
        public TeamType TeamType;
        public Vector3 Position;
        public Quaternion Rotation;
    }
}
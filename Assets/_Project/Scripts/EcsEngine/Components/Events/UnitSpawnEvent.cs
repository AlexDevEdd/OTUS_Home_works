using System;
using UnityEngine;


namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct UnitSpawnEvent
    {
        public Vector3 Position;
    }
}
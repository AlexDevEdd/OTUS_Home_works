using System;
using _Project.Scripts.EcsEngine.Enums;

namespace _Project.Scripts.EcsEngine.Components.Events
{
    [Serializable]
    public struct UnitSpawnRequest
    {
        public UnitType UnitType;
        public TeamType TeamType;
    }
    
    [Serializable]
    public struct GameOverEvent
    {
    }
}
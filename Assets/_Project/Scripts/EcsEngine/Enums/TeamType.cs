using System;

namespace _Project.Scripts.EcsEngine.Enums
{
    [Flags] [Serializable]
    public enum TeamType : byte
    {
        Default = 0,
        Red = 1,
        Blue = 2,
    }
}
using System;
using Sirenix.OdinInspector;

namespace _Project.Scripts
{
    [Serializable]
    public sealed class CharacterStat
    {
        public string Name;

        public int Value;

        public string GetValue()
        {
            return Value.ToString();
        }

    }
}
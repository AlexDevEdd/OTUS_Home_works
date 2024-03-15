using System;
using _Game.Scripts.Tools;

namespace _Project.Scripts.Components
{
    [Serializable]
    public sealed class PlayerHealthComponent
    {
        private float _health;
        
        public void UpgradeStat(float value)
        {
            _health = value;
            Log.ColorLog($"Health is {_health}", ColorType.Lime);
        }
    }
}
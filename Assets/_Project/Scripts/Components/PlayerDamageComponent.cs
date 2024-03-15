using System;
using _Game.Scripts.Tools;

namespace _Project.Scripts.Components
{
    [Serializable]
    public sealed class PlayerDamageComponent
    {
        private float _damage;
        
        public void UpgradeStat(float value)
        {
            _damage = value;
            Log.ColorLog($"Damage is {_damage}", ColorType.Red);
        }
    }
}
using System;
using _Game.Scripts.Tools;

namespace _Project.Scripts.Components
{
    [Serializable]
    public sealed class PlayerMoveComponent
    {
        private float _speed;
        
        public void UpgradeStat(float value)
        {
            _speed = value;
            Log.ColorLog($"Speed is {_speed}", ColorType.Lightblue);
        }
    }
}
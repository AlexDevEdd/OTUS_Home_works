using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Upgrades.Helpers
{
    [Serializable]
    public sealed class StatUpgradeTable
    {
        [SerializeField] private float _startValue;
        [SerializeField] private float _endValue;
        
        [ShowInInspector, ReadOnly]
        private float _step;

        private int _maxLevel;
        
        public float GetStatValue(int level)
        {
            if (level == 0) 
                return _startValue;
            
            return _startValue + _step * (level);
        }
        
        public float GetLinearInterpolationStatValue(int level)
        {
            if (level == 0) 
                return _startValue;
            
            var fraction = (float)level / _maxLevel;
            return  _startValue + fraction * (_endValue - _startValue);
        }
        
        public void OnValidate(int level)
        {
            _maxLevel = level;
            _step = (_endValue - _startValue) / level;
        }
    }
}
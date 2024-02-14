using System;
using _Project.Scripts.EcsEngine.Enums;
using Plugins.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.ScriptableConfigs
{
    [Serializable]
    public class GameBalance
    {
        [Title("Unit configuration", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField] 
        private SerializableDictionary<UnitType, UnitConfig> _unitConfig;
        
        public UnitConfig GetUnitConfigByClass(UnitType type)
        {
            if(_unitConfig.TryGetValue(type, out var config))
            {
                return config;
            }

            throw new ArgumentException($"Config with type of {type} doesn't exist");
        }
    }
    
    [Serializable]
    public struct UnitConfig
    {
        public float MoveSpeed;
        public float RotationSpeed;
        public int Health;
    }
}
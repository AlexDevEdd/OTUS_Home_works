using System;
using UnityEngine;

namespace _Project.Scripts.Upgrades.Configs
{
    [CreateAssetMenu(fileName = "UpgradeCatalog", menuName = "Upgrades/New UpgradeCatalog")]
    public sealed class UpgradeCatalog : ScriptableObject
    {
        [SerializeField]
        private UpgradeConfig[] _configs;
        
        public UpgradeConfig[] GetAllUpgrades()
        {
            return _configs;
        }

        public UpgradeConfig FindUpgrade(StatType statType)
        {
            var length = _configs.Length;
            for (var i = 0; i < length; i++)
            {
                var config = _configs[i];
                if (config.Id == statType)
                {
                    return config;
                }
            }

            throw new Exception($"Config with {statType} is not found!");
        }
    }
}
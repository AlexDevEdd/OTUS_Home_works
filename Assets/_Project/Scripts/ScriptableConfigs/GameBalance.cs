using System;
using _Project.Scripts.Upgrades;
using _Project.Scripts.Upgrades.Configs;
using Plugins.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.ScriptableConfigs
{
    [Serializable]
    public class GameBalance
    {
        public int StartMoney = 10000;
        
        [Title("App step configurations", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField] 
        private SerializableDictionary<int, AppStepConfig> _appStepConfigs;

        [Title("Upgrade Catalog", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField] 
        private UpgradeCatalog _upgradeCatalog;
        
        public AppStepConfig GetAppStepConfig(int id)
        {
            if(_appStepConfigs.TryGetValue(id, out var config))
                return config;

            throw new ArgumentException($"Config with type of {id} doesn't exist");
        }

        public UpgradeConfig[] GetAllUpgrades()
        {
            return _upgradeCatalog.GetAllUpgrades();
        }
        
        public UpgradeConfig FindUpgrade(StatType statType)
        {
            return _upgradeCatalog.FindUpgrade(statType);
        }
    }
}
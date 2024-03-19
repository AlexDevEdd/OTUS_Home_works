using System;
using System.Collections.Generic;
using _Project.Scripts.Upgrades.Configs;
using UnityEngine;

namespace _Project.Scripts.Upgrades.Stats
{
    public abstract class Upgrade
    {
        public event Action<int> OnLevelUp;

        private readonly UpgradeConfig Config;
        private int _currentLevel;
        
        public StatType Id => Config.Id;
        public string StatName => Config.StatName;
        
        public int Level => _currentLevel;
        
        public int MaxLevel => Config.MaxLevel;

        public bool IsMaxLevel => _currentLevel == Config.MaxLevel;
        
        public float Progress => (float) _currentLevel / Config.MaxLevel;
        
        public int NextPrice => Config.GetPrice(Level + 1);
        
        public Sprite Icon => Config.Icon;
        
        public IEnumerable<StatType> DependencyUpgrades => Config.GetLinkedUpgrades();

        
        protected Upgrade(UpgradeConfig config)
        {
            Config = config;
            _currentLevel = 1;
        }

        public void SetupLevel(int level)
        {
            _currentLevel = level;
        }

        public float GetStatValue(int level)
        {
            return Config.GetStatValue(level);
        }

        public void LevelUp()
        {
            if (Level >= MaxLevel)
            {
                throw new Exception($"Can not increment level for upgrade {Config.Id}!");
            }
            
            _currentLevel = Level + 1;
            LevelUp(_currentLevel);
            OnLevelUp?.Invoke(_currentLevel);
        }

        protected abstract void LevelUp(int level);
    }
}
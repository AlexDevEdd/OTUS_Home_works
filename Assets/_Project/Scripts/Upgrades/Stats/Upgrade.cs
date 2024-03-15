using System;
using System.Collections.Generic;
using _Project.Scripts.Upgrades.Configs;
using Sirenix.OdinInspector;

namespace _Project.Scripts.Upgrades.Stats
{
    public abstract class Upgrade
    {
        public event Action<int> OnLevelUp;

        protected readonly UpgradeConfig Config;
        private int _currentLevel;
        
        [ShowInInspector, ReadOnly]
        public StatType Id => Config.Id;

        [ShowInInspector, ReadOnly]
        public int Level => _currentLevel;

        [ShowInInspector, ReadOnly]
        public int MaxLevel => Config.MaxLevel;

        public bool IsMaxLevel => _currentLevel == Config.MaxLevel;

        [ShowInInspector, ReadOnly]
        public float Progress => (float) _currentLevel / Config.MaxLevel;

        [ShowInInspector, ReadOnly]
        public int NextPrice => Config.GetPrice(Level + 1);
        
        [ShowInInspector, ReadOnly] 
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
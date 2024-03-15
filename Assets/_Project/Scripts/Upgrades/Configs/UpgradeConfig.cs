using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Entities;
using _Project.Scripts.Upgrades.Helpers;
using _Project.Scripts.Upgrades.Stats;
using UnityEngine;

namespace _Project.Scripts.Upgrades.Configs
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        private const float SPACE_HEIGHT = 10.0f;
        
        [Space(SPACE_HEIGHT)]
        public string StatName;
        
        [Space(SPACE_HEIGHT)]
        public StatType Id;

        [Space(SPACE_HEIGHT)] [Range(2, 99)]
        public int MaxLevel = 2;

        [Space(SPACE_HEIGHT)]
        [SerializeField] private PriceTable _priceTable;
        
        [Space(SPACE_HEIGHT)]
        [SerializeField] private UpgradeConfig[] _dependencyUpgrades;
        
        [Space(SPACE_HEIGHT)]
        [SerializeField] protected StatUpgradeTable StatUpgradeTable;
        
        public abstract Upgrade InstantiateUpgrade(IEntity entity);
        public abstract float GetStatValue(int level);
        
        public int GetPrice(int level)
        {
            return _priceTable.GetPrice(level);
        }
        
        public IEnumerable<StatType> GetLinkedUpgrades()
        {
            return _dependencyUpgrades.Select(d => d.Id);
        }
        
        private void Validate()
        {
            _priceTable.OnValidate(MaxLevel);
            StatUpgradeTable.OnValidate(MaxLevel);
        }
        
        private void OnValidate()
        {
            try
            {
                Validate();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
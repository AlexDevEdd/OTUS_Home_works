using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Upgrades.Stats;
using JetBrains.Annotations;
using Zenject;

namespace _Project.Scripts.Upgrades
{
    [UsedImplicitly]
    public sealed class PlayerUpgradeSystem : IInitializable
    {
        public event Action<Upgrade> OnLevelUp;
        
        private Dictionary<StatType, Upgrade> _upgradesMap;
        private readonly MoneyStorage _moneyStorage;
        private readonly PlayerUpgradeStatFactory _playerUpgradeStatFactory;

        public PlayerUpgradeSystem(MoneyStorage moneyStorage, PlayerUpgradeStatFactory playerUpgradeStatFactory)
        {
            _moneyStorage = moneyStorage;
            _playerUpgradeStatFactory = playerUpgradeStatFactory;
        }
        
        public void Initialize()
        {
            _upgradesMap = new Dictionary<StatType, Upgrade>();
            var upgrades = _playerUpgradeStatFactory.CreateAllUpgrades();
            foreach (var upgrade in upgrades)
            {
                _upgradesMap.TryAdd(upgrade.Id, upgrade);
            }
        }
        
        public Upgrade GetUpgrade(StatType id)
        {
            return _upgradesMap[id];
        }

        public Upgrade[] GetAllUpgrades()
        {
            return _upgradesMap.Values.ToArray();
        }

        public bool CanLevelUp(Upgrade upgrade)
        {
            if (upgrade.IsMaxLevel || upgrade.DependencyUpgrades
                    .Any(value => _upgradesMap[value].Level < upgrade.Level))
            {
                return false;
            }

            var price = upgrade.NextPrice;
            return _moneyStorage.CanSpendMoney(price);
        }

        public void LevelUp(Upgrade upgrade)
        {
            if (!CanLevelUp(upgrade))
            {
                throw new Exception($"Can not level up {upgrade.Id}");
            }
            
            var price = upgrade.NextPrice;
            _moneyStorage.SpendMoney(price);
            
            upgrade.LevelUp();
            OnLevelUp?.Invoke(upgrade);
        }
        
        public bool CanLevelUp(StatType id)
        {
            var upgrade = _upgradesMap[id];
            return CanLevelUp(upgrade);
        }
        
        public void LevelUp(StatType id)
        {
            var upgrade = _upgradesMap[id];
            LevelUp(upgrade);
        }
    }
}
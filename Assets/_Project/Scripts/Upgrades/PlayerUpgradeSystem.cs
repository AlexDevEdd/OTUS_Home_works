using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.UI.Upgrades;
using _Project.Scripts.Upgrades.Stats;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Zenject;

namespace _Project.Scripts.Upgrades
{
    [UsedImplicitly] [Serializable]
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
        
        public bool IsMaxLevel(StatType id)
        {
            var upgrade = _upgradesMap[id];
            return upgrade.IsMaxLevel;
        }

        private bool CanLevelUp(Upgrade upgrade)
        {
            if (upgrade.IsMaxLevel || upgrade.DependencyUpgrades
                    .Any(value => _upgradesMap[value].Level < upgrade.Level))
            {
                return false;
            }

            var price = upgrade.NextPrice;
            return _moneyStorage.CanSpendMoney(price);
        }

        private void LevelUp(Upgrade upgrade)
        {
            var price = upgrade.NextPrice;
            _moneyStorage.SpendMoney(price);
            
            upgrade.LevelUp();
            OnLevelUp?.Invoke(upgrade);
        }

        private UpgradeUIPresenter _presenter;
        
        [Button]
        public void CreateUpgrade(StatType id)
        {
            var upgrade = _upgradesMap[id];
            _presenter = new UpgradeUIPresenter(upgrade, this, _moneyStorage);
        }

        [Button]
        public void Show(UpgradeView view)
        {
            view.Show(_presenter);
        }
    }
}
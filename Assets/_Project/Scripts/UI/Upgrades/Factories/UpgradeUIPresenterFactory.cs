using _Project.Scripts.UI.Upgrades.Presenters;
using _Project.Scripts.Upgrades;
using JetBrains.Annotations;

namespace _Project.Scripts.UI.Upgrades.Factories
{
    [UsedImplicitly]
    public sealed class UpgradeUIPresenterFactory
    {
        private readonly MoneyStorage _moneyStorage;
        private readonly PlayerUpgradeSystem _upgradeSystem;

        public UpgradeUIPresenterFactory(MoneyStorage moneyStorage, PlayerUpgradeSystem upgradeSystem)
        {
            _moneyStorage = moneyStorage;
            _upgradeSystem = upgradeSystem;
        }

        public IUpgradePresenter CreateUpgradePresenter(StatType id)
        {
            var upgrade = _upgradeSystem.GetUpgrade(id);
            return new UpgradeUIPresenter(upgrade, _upgradeSystem, _moneyStorage);
        }
    }
}
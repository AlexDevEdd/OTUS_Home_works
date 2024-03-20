using System;
using System.Collections.Generic;
using _Project.Scripts.UI.Upgrades.Factories;
using _Project.Scripts.UI.Upgrades.Presenters;
using _Project.Scripts.UI.Upgrades.Views;
using _Project.Scripts.Upgrades;
using JetBrains.Annotations;
using Zenject;

namespace _Project.Scripts.UI.Upgrades
{
    [UsedImplicitly] [Serializable]
    public sealed class PlayerUpgradePopUpSystem : IInitializable
    {
        private readonly UpgradeUIPresenterFactory _upgradeUIPresenterFactory;
        private readonly PlayerUpgradeViewFactory _upgradeViewFactory;
        private readonly PlayerUpgradeSystem _upgradeSystem;
        private readonly UpgradePopUpView _upgradePopUpView;
        private readonly MoneyStorage _moneyStorage;
        
        private IUpgradePopUpPresenter _upgradePopUpPresenter;
        
        public readonly List<UpgradeView> UpgradeViews = new ();
        public readonly List<IUpgradePresenter> UpgradePresenters = new ();
        
        public PlayerUpgradePopUpSystem(PlayerUpgradeViewFactory upgradeViewFactory, PlayerUpgradeSystem upgradeSystem,
            UpgradeUIPresenterFactory upgradeUIPresenterFactory, UpgradePopUpView upgradePopUpView, MoneyStorage moneyStorage)
        {
            _upgradeUIPresenterFactory = upgradeUIPresenterFactory;
            _upgradeViewFactory = upgradeViewFactory;
            _upgradeSystem = upgradeSystem;
            _upgradePopUpView = upgradePopUpView;
            _moneyStorage = moneyStorage;
        }
        
        public void Initialize()
        {
            foreach (var upgrade in _upgradeSystem.GetAllUpgrades())
            {
                UpgradeViews.Add( _upgradeViewFactory.CreateUpgradeView());
                UpgradePresenters.Add( _upgradeUIPresenterFactory.CreateUpgradePresenter(upgrade.Id));
            }

            _upgradePopUpPresenter = new UpgradePopUpPresenter(this, _moneyStorage);
        }
        
        public void ShowPopUp()
        {
            _upgradePopUpView.Show(_upgradePopUpPresenter);
        }
        
        public void ClosePopUp()
        {
            _upgradePopUpView.Hide();
        }
    }
}
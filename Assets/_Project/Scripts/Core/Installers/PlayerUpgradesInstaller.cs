using _Project.Scripts.UI.Upgrades;
using _Project.Scripts.UI.Upgrades.Factories;
using _Project.Scripts.UI.Upgrades.Views;
using _Project.Scripts.Upgrades;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Installers
{
    public sealed class PlayerUpgradesInstaller : BaseInstaller
    {
        public PlayerUpgradesInstaller(DiContainer container, Transform _upgradeUiContainer,
            UpgradePopUpView _upgradePopUpView) : base(container)
        {
            BindInterfacesAndSelfTo<PlayerUpgradeStatFactory>(container);
            BindInterfacesAndSelfTo<PlayerUpgradeSystem>(container);
            BindInterfacesAndSelfTo<UpgradeUIPresenterFactory>(container);
            BindInterfacesAndSelfToWithArguments<PlayerUpgradeViewFactory, Transform>(container, _upgradeUiContainer);
            BindInterfacesAndSelfToWithArguments<PlayerUpgradePopUpSystem, UpgradePopUpView>(container, _upgradePopUpView);
        }
    }
}
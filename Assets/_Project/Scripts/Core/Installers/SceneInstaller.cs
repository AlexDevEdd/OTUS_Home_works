using _Project.Scripts.Entities;
using _Project.Scripts.UI.Upgrades;
using _Project.Scripts.UI.Upgrades.Views;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity _player;
        [SerializeField] private Transform _upgradeUiContainer;
        [SerializeField] private UpgradePopUpView _upgradePopUpView;

        public override void InstallBindings()
        {
            BindPlayerEntity();
            BindMoneyStorage();

            new PlayerUpgradesInstaller(Container, _upgradeUiContainer, _upgradePopUpView);
        }

        private void BindPlayerEntity()
        {
            Container.BindInterfacesAndSelfTo<PlayerEntity>()
                .FromInstance(_player)
                .AsSingle()
                .NonLazy();
        }

        private void BindMoneyStorage()
        {
            Container.BindInterfacesAndSelfTo<MoneyStorage>()
                .AsSingle()
                .NonLazy();
        }
    }
}
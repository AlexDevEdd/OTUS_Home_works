using _Project.Scripts.Entities;
using _Project.Scripts.UI.Upgrades;
using _Project.Scripts.Upgrades;
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
            Container.BindInterfacesAndSelfTo<PlayerUpgradeStatFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MoneyStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerUpgradeSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerEntity>().FromInstance(_player).AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerUpgradeViewFactory>()
                .AsSingle()
                .WithArguments(_upgradeUiContainer)
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<UpgradeUIPresenterFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerUpgradePopUpSystem>()
                .AsSingle()
                .WithArguments(_upgradePopUpView)
                .NonLazy();
        }
    }
}
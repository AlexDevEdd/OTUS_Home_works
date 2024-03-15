using _Project.Scripts.Entities;
using _Project.Scripts.Upgrades;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity _player;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerUpgradeStatFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MoneyStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerUpgradeSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerEntity>().FromInstance(_player).AsSingle().NonLazy();
        }
    }
}
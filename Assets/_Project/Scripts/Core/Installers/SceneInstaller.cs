using _Project.Scripts.Entities;
using _Project.Scripts.Inventory;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity _player;
        [SerializeField] private Transform _container;
        
        public override void InstallBindings()
        {
            BindPlayerEntity();
            Container.BindInterfacesAndSelfTo<InventorySystem>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerEntity()
        {
            Container.BindInterfacesAndSelfTo<PlayerEntity>()
                .FromInstance(_player)
                .AsSingle()
                .NonLazy();
        }
    }
}
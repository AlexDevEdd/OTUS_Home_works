using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine._OOP.Factories;
using Leopotam.EcsLite.Entities;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsStartup>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<EntityManager>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<UnitFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}
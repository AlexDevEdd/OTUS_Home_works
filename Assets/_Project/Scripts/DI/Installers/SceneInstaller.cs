using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Factories;
using Leopotam.EcsLite.Entities;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EntityManager>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<EcsStartup>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<UnitFactory>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<WarriorRedFactory>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<WarriorBlueFactory>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ArcherRedFactory>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ArcherBlueFactory>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<UnitSystem>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<VfxSystem>()
                .AsSingle()
                .NonLazy(); 
            
            Container.BindInterfacesAndSelfTo<VfxSoulFactory>()
                .AsSingle()
                .WithArguments(VfxType.Soul)
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<VfxSpawnUnitFactory>()
                .AsSingle()
                .WithArguments(VfxType.Spawn)
                .NonLazy();
        }
    }
}
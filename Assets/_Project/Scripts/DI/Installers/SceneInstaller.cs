using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine._OOP.UI.TeamPanel;
using _Project.Scripts.EcsEngine._OOP.UI.Views;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private TeamPanelUI teamPanelUI;
        [SerializeField] private GameOverWindow _gameOverWindow;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsAdmin>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameOverWindow>()
                .FromInstance(_gameOverWindow)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<TeamPanelUI>()
                .FromInstance(teamPanelUI)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<TeamPanelSystem>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<TeamPanelPresenterFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}
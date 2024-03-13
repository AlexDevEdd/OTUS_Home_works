using _Project.Scripts.BootStrap;
using _Project.Scripts.BootStrap.Steps;
using _Project.Scripts.UI.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Installers
{
    public sealed class BootStrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingWindow _loadingWindow;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootStrapSystem>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<LoadingWindow>()
                .FromInstance(_loadingWindow)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<Test_FirstAppStep>()
                .AsSingle()
                .NonLazy();
        }
    }
}
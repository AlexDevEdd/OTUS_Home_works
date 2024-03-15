using _Project.Scripts.BootStrap;
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
            BindBootStrapSystem();
            BindLoadingWindow();
            
            new AppStepInstaller(Container);
        }

        private void BindBootStrapSystem()
        {
            Container.BindInterfacesAndSelfTo<BootStrapSystem>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindLoadingWindow()
        {
            Container.Bind<LoadingWindow>()
                .FromInstance(_loadingWindow)
                .AsSingle()
                .NonLazy();
        }
    }
}
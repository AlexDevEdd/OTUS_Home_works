using _Project.Scripts.EcsEngine;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsStartup>()
                .AsSingle()
                .NonLazy();
        }
    }
}
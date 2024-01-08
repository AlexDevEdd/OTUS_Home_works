using Bullets;
using Zenject;

namespace GameCore.Installers
{
    public sealed class BulletInstaller
    {
        public BulletInstaller(DiContainer container)
        {
            BindBulletFactory(container);
            BindBulletSystem(container);
        }
        
        private void BindBulletFactory(DiContainer container)
        {
            container.Bind<BulletFactory>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindBulletSystem(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<BulletSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}
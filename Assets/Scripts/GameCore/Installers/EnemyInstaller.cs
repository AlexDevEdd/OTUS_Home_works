using Enemies;
using Zenject;

namespace GameCore.Installers
{
    public sealed class EnemyInstaller
    {
        public EnemyInstaller(DiContainer container)
        {
            BindEnemySpawner(container);
            BindEnemySystem(container);
        }
        
        private void BindEnemySpawner(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<EnemySpawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemySystem(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<EnemySystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}
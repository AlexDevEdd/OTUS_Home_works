using Components;
using Zenject;

namespace GameCore.Installers
{
    public sealed class GameManagerInstaller
    {
        public GameManagerInstaller(DiContainer container)
        {
            BindGameManager(container);
            BindPauseResumeInputListener(container);
            BindPlayerDeathObserver(container);
        }
        
        private void BindGameManager(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<GameManager>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPauseResumeInputListener(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<PauseResumeInputListener>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPlayerDeathObserver(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<PlayerDeathObserver>()
                .AsSingle()
                .NonLazy();
        }
    }
}
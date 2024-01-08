using Character;
using Zenject;

namespace GameCore.Installers
{
    public sealed class PlayerInstaller
    {
        public PlayerInstaller(DiContainer container, Player player)
        {
            BindPlayer(container, player);
        }
        
        private void BindPlayer(DiContainer container, Player player)
        {
            container.BindInterfacesAndSelfTo<Player>()
                .FromInstance(player)
                .AsSingle()
                .NonLazy();
        }
    }
}
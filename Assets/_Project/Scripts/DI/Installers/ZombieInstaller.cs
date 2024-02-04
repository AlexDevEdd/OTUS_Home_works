using _Project.Scripts.GameEngine.Controllers;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Factories;
using _Project.Scripts.Gameplay.Spawners;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class ZombieInstaller
    {
        public ZombieInstaller(DiContainer container, ZombieSpawner spawner)
        {
            BindZombieFactory(container);
            BindZombieSpawnSystem(container);
            BindZombieSpawner(container, spawner);
        }

        private void BindZombieFactory(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<ZombieFactory>().AsSingle().NonLazy();
        }
        
        private void BindZombieSpawnSystem(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<ZombieSpawnSystem>().AsSingle().NonLazy();
        } 
        
        private void BindZombieSpawner(DiContainer container, ZombieSpawner spawner)
        {
            container.BindInterfacesAndSelfTo<ZombieSpawner>()
                .FromInstance(spawner)
                .AsSingle()
                .NonLazy();
        }
    }
}
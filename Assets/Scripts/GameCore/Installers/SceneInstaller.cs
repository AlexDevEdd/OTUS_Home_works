using Bullets;
using Character;
using Components;
using Enemies;
using Systems.InputSystem;
using Systems.Points;
using UnityEngine;
using Zenject;

namespace GameCore
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private Transform _pointsRoot;

        public override void InstallBindings()
        {
            BindInput();
            BindGameManager();
            BindPlayer();
            BindPointSystem();
            BindBulletFactory();
            BindBulletSystem();
            BindEnemySystem();
            BindEnemySpawner();
            BindPauseResumeInputListener();
            BindPlayerDeathObserver();
        }

        private void BindGameManager()
        {
            Container.BindInterfacesAndSelfTo<GameManager>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemySpawner()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEnemySystem()
        {
            Container.BindInterfacesAndSelfTo<EnemySystem>()
                .AsSingle()
                .NonLazy();
        }

        private void BindBulletSystem()
        {
            Container.BindInterfacesAndSelfTo<BulletSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPauseResumeInputListener()
        {
            Container.BindInterfacesAndSelfTo<PauseResumeInputListener>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<InputReader>()
                .AsSingle()
                .NonLazy();
        }
        private void BindPlayerDeathObserver()
        {
            Container.BindInterfacesAndSelfTo<PlayerDeathObserver>()
                .AsSingle()
                .NonLazy();
        }

        private void BindBulletFactory()
        {
            Container.Bind<BulletFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPointSystem()
        {
            Container.BindInterfacesAndSelfTo<PointSystem>()
                .AsSingle()
                .WithArguments(_pointsRoot)
                .NonLazy();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<Player>()
                .FromInstance(_player)
                .AsSingle()
                .NonLazy();
        }
    }
}
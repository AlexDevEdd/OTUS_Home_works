using Character;
using UnityEngine;
using Zenject;

namespace GameCore.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private Transform _pointsRoot;

        public override void InstallBindings()
        {
            new InputInstaller(Container);
            new GameManagerInstaller(Container);
            new PlayerInstaller(Container, _player);
            new BulletInstaller(Container);
            new EnemyInstaller(Container);
            new PointInstaller(Container, _pointsRoot);
        }
    }
}
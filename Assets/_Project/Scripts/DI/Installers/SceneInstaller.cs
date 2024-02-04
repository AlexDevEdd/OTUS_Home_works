using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Character;
using _Project.Scripts.Gameplay.Spawners;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Character _character;
        [SerializeField] private ZombieSpawner _zombieSpawner;

        public override void InstallBindings()
        {
            new CharacterInstaller(Container, _character);
            new ZombieInstaller(Container, _zombieSpawner);
            new AudioSystemInstaller(Container);
        }
    }
}
using _Project.Scripts.GameEngine.Controllers;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Character;
using _Project.Scripts.Gameplay.Factories;
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
            
            Container.BindInterfacesAndSelfTo<MoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MouseRotateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<FireController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<AudioSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ZombieFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ZombieSpawnSystem>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<ZombieSpawner>()
                .FromInstance(_zombieSpawner)
                .AsSingle()
                .NonLazy();
            
        }
    }

    public sealed class CharacterInstaller
    {
        public CharacterInstaller(DiContainer container, Character character)
        {
            BindCharacter(container, character);
        }

        private void BindCharacter(DiContainer container, Character character)
        {
            container.BindInterfacesAndSelfTo<Character>()
                .FromInstance(character)
                .AsSingle()
                .NonLazy();
        }
    }
}
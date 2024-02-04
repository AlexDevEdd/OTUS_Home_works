using _Project.Scripts.GameEngine.Controllers;
using _Project.Scripts.Gameplay.Character;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class CharacterInstaller
    {
        public CharacterInstaller(DiContainer container, Character character)
        {
            BindCharacter(container, character);
            BindMoveController(container);
            BindMouseRotateController(container);
            BindFireController(container);
        }

        private void BindCharacter(DiContainer container, Character character)
        {
            container.BindInterfacesAndSelfTo<Character>()
                .FromInstance(character)
                .AsSingle()
                .NonLazy();
        }

        private void BindMoveController(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<MoveController>().AsSingle().NonLazy();
        }
        
        private void BindMouseRotateController(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<MouseRotateController>().AsSingle().NonLazy();
        }
        
        private void BindFireController(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<FireController>().AsSingle().NonLazy();
        }
    }
}
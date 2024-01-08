using Systems.InputSystem;
using Zenject;

namespace GameCore.Installers
{
    public sealed class InputInstaller
    {
        public InputInstaller(DiContainer container)
        {
            BindInput(container);
        }
        
        private void BindInput(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<InputReader>()
                .AsSingle()
                .NonLazy();
        }
    }
}
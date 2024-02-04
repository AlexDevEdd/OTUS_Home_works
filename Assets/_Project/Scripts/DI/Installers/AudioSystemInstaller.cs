using _Project.Scripts.GameEngine.Controllers;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class AudioSystemInstaller
    {
        public AudioSystemInstaller(DiContainer container)
        {
            BindAudioSystem(container);
        }
        
        private void BindAudioSystem(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<AudioSystem>().AsSingle().NonLazy();
        }
    }
}
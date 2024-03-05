using _Project.Scripts.EcsEngine._OOP.UI.Views;
using _Project.Scripts.EcsEngine.Components.Events;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.AnimatorListeners
{
    internal sealed class GameOverListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GameOverEvent>> _filter = EcsWorlds.Events;
        
        private readonly EcsCustomInject<GameOverWindow> _gameOverWindow;
        
        private readonly EcsWorldInject _world = EcsWorlds.Events;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _gameOverWindow.Value.Show();
                _world.Value.DelEntity(entity);
            }
        }
    }
}
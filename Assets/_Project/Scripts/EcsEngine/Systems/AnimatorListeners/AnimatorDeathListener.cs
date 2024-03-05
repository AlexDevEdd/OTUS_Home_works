using _Game.Scripts.Tools;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine._OOP.UI.Views;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.AnimatorListeners
{
    internal sealed class AnimatorDeathListener : IEcsRunSystem
    {
        private static readonly int Death = Animator.StringToHash("Death");

        private readonly EcsFilterInject<Inc<AnimatorView, DeathEvent>> _filter;
        
        private readonly EcsPoolInject<AnimatorView> _animatorPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _animatorPool.Value.Get(entity).Value.SetTrigger(Death);
            }
        }
    }
    
    internal sealed class BuildingDeathListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BuildingTag, DeathEvent>> _filter;

        private readonly EcsPoolInject<TransformView> _transformView;
        
        private readonly EcsCustomInject<TeamPanelSystem> _teamPanelSystem;
        private readonly EcsCustomInject<EcsAdmin> _ecsAdmin;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
               var transformView =  _transformView.Value.Get(entity);
               transformView.Value.gameObject.SetActive(false);
               _teamPanelSystem.Value.ClosePanel();
               _ecsAdmin.Value.CreateEntity(EcsWorlds.Events)
                   .Add(new GameOverEvent());
            }
        }
    }
    
    internal sealed class GameOverListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GameOverEvent>> _filter;
        
        private readonly EcsCustomInject<GameOverWindow> _gameOverWindow;
        
        private readonly EcsWorldInject _world = EcsWorlds.Events;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _world.Value.DelEntity(entity);
                _gameOverWindow.Value.Show();
            }
        }
    }
}
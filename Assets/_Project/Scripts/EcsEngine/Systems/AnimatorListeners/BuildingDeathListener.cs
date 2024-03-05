using _Project.Scripts.EcsEngine._OOP.UI.TeamPanel;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.AnimatorListeners
{
    internal sealed class BuildingDeathListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BuildingTag, DeathEvent>> _filter;

        private readonly EcsPoolInject<TransformView> _transformView;
        
        private readonly EcsCustomInject<TeamPanelUI> _teamPanel;
        private readonly EcsCustomInject<EcsAdmin> _ecsAdmin;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var transformView =  _transformView.Value.Get(entity);
                transformView.Value.gameObject.SetActive(false);
                
                _teamPanel.Value.Hide();
                
                _ecsAdmin.Value.CreateEntity(EcsWorlds.Events)
                    .Add(new GameOverEvent());
            }
        }
    }
}
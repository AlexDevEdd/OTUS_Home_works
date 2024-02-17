using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using _Project.Scripts.EcsEngine.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class DeathRequestSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DeathRequest>, Exc<Inactive>> _filter;

        private EcsPoolInject<DeathEvent> _eventPool;
        private EcsPoolInject<Inactive> _inactivePool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);

                _inactivePool.Value.Add(entity) = new Inactive();
                _eventPool.Value.Add(entity) = new DeathEvent();
            }
        }
    }

    internal sealed class SpawnUnitSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SpawnerTag, UnitSpawnRequest>> _filter;
        private readonly EcsPoolInject<UnitSpawnRequest> _unitSpawnRequestPool;
        
        private EcsCustomInject<UnitSystem> _unitSystem;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var request = _unitSpawnRequestPool.Value.Get(entity);
                SpawnUnit(request);
                _unitSpawnRequestPool.Value.Del(entity);
            }
        }

        private void SpawnUnit(UnitSpawnRequest request)
        {
            _unitSystem.Value.Spawn(request.UnitType, request.TeamType, request.Position, request.Rotation);
        }
    }
    
    internal sealed class FindTargetEntitySystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TransformView, TargetEntity, Team, FindTargetRequest>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<FindTargetRequest> _findTargetRequestPool;
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        private readonly EcsPoolInject<Team> _teamPool;
        
        private EcsCustomInject<UnitSystem> _unitSystem;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var transformView = _transformViewPool.Value.Get(entity);
                Entity targetEntity = default;
                switch (_teamPool.Value.Get(entity).Value)
                {
                    case TeamType.Red:
                         targetEntity = _unitSystem.Value.GetClosestByTeam(transformView, TeamType.Blue);
                        break;
                    case TeamType.Blue:
                        targetEntity = _unitSystem.Value.GetClosestByTeam(transformView, TeamType.Red);
                        break;
                }
                
               
                
                if(targetEntity == default || targetEntity == null) continue;
                
                ref var ownerTargetEntity = ref _targetPool.Value.Get(entity);
                ownerTargetEntity.Id = targetEntity.Id;
                ownerTargetEntity.Transform = targetEntity.transform;
                
                _findTargetRequestPool.Value.Del(entity);
                
            }
        }

        private void Test()
        {
            
        }
    }
}

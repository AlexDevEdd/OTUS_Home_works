using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class DeathRequestSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DeathRequest>, Exc<Inactive>> _filter;

        private EcsPoolInject<DeathEvent> _eventPool;
        private EcsPoolInject<Inactive> _inactivePool;
        
        private EcsCustomInject<UnitFactory> _unitFactory;
        private EcsCustomInject<EntityManager> _entityManager;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);

                _inactivePool.Value.Add(entity) = new Inactive();
                _eventPool.Value.Add(entity) = new DeathEvent();
                //_unitFactory.Value.DeSpawn(entity);
            }
        }
    }
    
    internal sealed class DeSpawnRequestSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DeSpawnRequest, Inactive>> _filter;
        
        private EcsCustomInject<UnitFactory> _unitFactory;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);
                _unitFactory.Value.DeSpawn(entity);
            }
        }
    }
    
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

    internal sealed class SpawnUnitSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SpawnerTag, UnitSpawnRequest>> _filter;
        private readonly EcsPoolInject<UnitSpawnRequest> _unitSpawnRequestPool;
        
        private EcsCustomInject<UnitFactory> _unitFactory;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var request = _unitSpawnRequestPool.Value.Get(entity);
                SpawnUnit(request);
            }
        }

        private void SpawnUnit(UnitSpawnRequest request)
        {
            _unitFactory.Value.Spawn(request.UnitType, request.TeamType, request.Position, request.Rotation).Forget();
        }
    }
    
    //private readonly EcsFilterInject<Inc<UnitSpawnRequest, SpawnPoint, UnitPrefab, Team, SpawnTimeout, SpawnTimeoutCurrent>> _filter;
        //private readonly EcsFilterInject<Inc<UnitPoolContainerTag, Container>> _poolFilter;
       // private readonly EcsCustomInject<EntityManager> _entityManager;
       // private readonly EcsPoolInject<SpawnUnitEvent> _spawnUnitEventPool;
    //     public void Run (IEcsSystems systems) 
    //     {
    //         var poolEntity = -1;
    //         foreach (var entity in _poolFilter.Value)
    //         {
    //             poolEntity = entity;
    //         }
    //
    //         if (poolEntity == -1)
    //         {
    //             throw new Exception("No unit pool container found!");
    //         }
    //         foreach (var entity in _filter.Value)
    //         {
    //             ref var timeout = ref _filter.Pools.Inc6.Get(entity);
    //             if(timeout.value > 0)
    //                 continue;
    //             var unit = _filter.Pools.Inc3.Get(entity);
    //             _entityManager.Value.Create(unit.value, _filter.Pools.Inc2.Get(entity).value.position,
    //                 unit.value.transform.rotation, _poolFilter.Pools.Inc2.Get(poolEntity).value);
    //             _filter.Pools.Inc1.Del(entity);
    //             timeout.value = _filter.Pools.Inc5.Get(entity).value;
    //             _spawnUnitEventPool.Value.Add(entity);
    //         }
     }

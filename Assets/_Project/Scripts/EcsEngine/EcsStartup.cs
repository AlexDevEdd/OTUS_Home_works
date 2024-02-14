using System;
using System.Collections.Generic;
using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine.Components.Events;
using _Project.Scripts.EcsEngine.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Helpers;
using Zenject;

namespace _Project.Scripts.EcsEngine
{
    public sealed class EcsStartup : IInitializable, ITickable, IDisposable
    {
        private EcsWorld _world;
        private EcsWorld _events;
        
        private IEcsSystems _systems;
        
        private readonly EntityManager _entityManager;
        
        private readonly object[] _customInjectObjects;
        private readonly HashSet<EcsWorld> _ecsWorlds;
        

        [Inject]
        public EcsStartup(EntityManager entityManager, UnitFactory unitFactory)
        {
            _entityManager = entityManager;
            _customInjectObjects = new object[]
            {
                _entityManager, unitFactory
            };
            
            _ecsWorlds = new HashSet<EcsWorld>();
        }
        
        
        public EcsEntityBuilder CreateEntity(string worldName = null)
        {
            return new EcsEntityBuilder(_systems.GetWorld(worldName));
        }
        
        public void Initialize()
        {
            _world = new EcsWorld();
            _events = new EcsWorld();
            _ecsWorlds.Add(_world);
            _ecsWorlds.Add(_events);
            
            _systems = new EcsSystems(_world);
            
            _systems.AddWorld(_events, EcsWorlds.Events);
            
            _systems
                .Add(new SpawnUnitSystem())
                .Add(new UnitMovementSystem())
                .Add(new TransformViewSynchronizer())
                .Add(new HealthEmptySystem())
                .Add(new DeathRequestSystem())
                .Add(new AnimatorDeathListener())
                .Add(new DeSpawnRequestSystem())

#if UNITY_EDITOR

                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .DelHere<DeathEvent>();
            _entityManager.Initialize(_world);
            _systems.Inject(_customInjectObjects);
            _systems.Init(); 
        }

        public void Tick()
        {
            _systems?.Run();
        }

        public void Dispose()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_ecsWorlds.Count > 0) 
                DestroyWorlds();
        }

        private void DestroyWorlds()
        {
            for (var i = 0; i < _ecsWorlds.Count; i++)
            {
                var world = _ecsWorlds.GetEnumerator().Current;
                world?.Destroy();
            }
            
            _events = null;
            _world = null;
        }

        // public EcsWorld GetWorld(string worldName = null)
        // {
        //     return worldName switch
        //     {
        //         null => _world,
        //         EcsWorlds.Events => _events,
        //         _ => throw new Exception($"World with name {worldName} is not found!")
        //     };
        // }
    }
}
using _Project.Scripts.GameEngine.Interfaces;
using _Project.Scripts.Tools;
using Atomic.Objects;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Extensions.Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameEngine.Controllers
{
    public sealed class ZombieSpawnSystem : IInitializable
    {
        private readonly IZombieFactory _zombieFactory;
        private readonly AtomicObject _spawner;
        private IAtomicObservable _spawnEvent;
        private Transform[] _spawnPoints;

        [Inject]
        public ZombieSpawnSystem(IZombieSpawner spawner, IZombieFactory zombieFactory)
        {
            _zombieFactory = zombieFactory;
            _spawner = spawner as AtomicObject;
        }

        public void Initialize()
        {
            _spawnPoints = _spawner.Get<Transform[]>(ObjectAPI.Transforms);
            _spawnEvent = _spawner.GetObservable(ObjectAPI.SpawnEvent);
            _spawnEvent.Subscribe(OnSpawn);
        }

        private void OnSpawn()
        {
            var randomPoint = _spawnPoints.GetRandomElement();
            _zombieFactory.Create(randomPoint.position, randomPoint.rotation);
        }
    }
}
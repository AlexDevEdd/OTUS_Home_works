using _Project.Scripts.GameEngine.Interfaces;
using _Project.Scripts.Tools;
using Atomic.Objects;
using Plugins.Atomic.Elements.Scripts.Interfaces;
using Plugins.Atomic.Extensions.Scripts;
using Zenject;

namespace _Project.Scripts.GameEngine.Controllers
{
    public sealed class ZombieSpawnSystem : IInitializable
    {
        private readonly AtomicObject _spawner;
        private IAtomicObservable _spawnEvent;

        [Inject]
        public ZombieSpawnSystem(IZombieSpawner spawner)
        {
            _spawner = spawner as AtomicObject;
        }

        public void Initialize()
        {
            _spawnEvent = _spawner.GetObservable(ObjectAPI.SpawnEvent);
            _spawnEvent.Subscribe(OnSpawn);
        }

        private void OnSpawn()
        {
            Log.ColorLog($"SPAWN", ColorType.Green, LogStyle.Error);
        }
    }
}
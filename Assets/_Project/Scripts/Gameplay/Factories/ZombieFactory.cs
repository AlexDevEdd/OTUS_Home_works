using System;
using System.Collections.Generic;
using _Project.Scripts.GameEngine;
using _Project.Scripts.GameEngine.Interfaces;
using _Project.Scripts.Gameplay.Units;
using Atomic.Objects;
using Plugins.Atomic.Objects.Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Factories
{
    public class ZombieFactory : IInitializable , IZombieFactory
    {
        private const int POOL_SIZE = 20;
        private const string ZOMBIE_KEY = "zombie";
        
        private readonly HashSet<IZombie> _cachedBullets = new(POOL_SIZE);
        private readonly IPrefabProvider _prefabProvider;
        private readonly IAudioSystem _audioSystem;
        private readonly AtomicObject _character;
        
        private Pool<Zombie> _pool;
        private Transform _target;
        
        [Inject]
        public ZombieFactory(IPrefabProvider prefabProvider , ICharacter character, IAudioSystem audioSystem)
        {
            _prefabProvider = prefabProvider;
            _audioSystem = audioSystem;
            _character = character as AtomicObject;
        }
        
        public void Initialize()
        {
            _target = _character.Get<Transform>(ObjectAPI.Transform);
            CreatePool(_prefabProvider.GetPrefab<Zombie>(ZOMBIE_KEY));
        }

        private void CreatePool(Zombie prefab)
        {
             _pool = new Pool<Zombie>(prefab, POOL_SIZE);
        }

        public void Create(Vector3 position, Quaternion rotation)
        {
            var zombie = _pool.Spawn();
            var gameObject = zombie.gameObject;
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            zombie.DeSpawnEvent.Subscribe(OnDeSpawn);
            zombie.Init(_target, _audioSystem);
            _cachedBullets.Add(zombie);
        }

        private void OnDeSpawn(IAtomicObject obj)
        {
            var zombie = obj as Zombie;
            if (zombie != null)
            {
                zombie.DeSpawnEvent.Unsubscribe(OnDeSpawn);
                Remove(zombie);
            }
        }

        public void Remove(IZombie zombie)
        {
            if (_cachedBullets.Contains(zombie))
            {
                _pool.DeSpawn(zombie as Zombie);
                _cachedBullets.Remove(zombie);
            }
            else
                throw new ArgumentException($"you're trying to remove {zombie} twice");
        }
    }
}
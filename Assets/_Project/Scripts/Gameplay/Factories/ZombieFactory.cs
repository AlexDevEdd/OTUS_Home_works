using System;
using System.Collections.Generic;
using _Project.Scripts.GameEngine.Interfaces;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Factories
{
    public class ZombieFactory : IInitializable , IZombieFactory
    {
        private const int POOL_SIZE = 20;
        private const string ZOMBIE_KEY = "zombie";
        
        private Pool<Zombie> _pool;
        private readonly IPrefabProvider _prefabProvider;
        private readonly HashSet<IZombie> _cachedBullets = new(POOL_SIZE);
        
        [Inject]
        public ZombieFactory(IPrefabProvider prefabProvider)
        {
            _prefabProvider = prefabProvider;
        }
        
        public void Initialize()
        {
            CreatePool(_prefabProvider.GetPrefab<Zombie>(ZOMBIE_KEY));
        }
        
        private void CreatePool(Zombie prefab)
            => _pool = new Pool<Zombie>(prefab, POOL_SIZE);

        public void Create(Vector3 position, Quaternion rotation)
        {
            var zombie = _pool.Spawn();
            zombie.gameObject.transform.position = position;
            zombie.gameObject.transform.rotation = rotation;
            
            _cachedBullets.Add(zombie);
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
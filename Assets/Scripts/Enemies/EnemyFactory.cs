using System;
using System.Collections.Generic;
using Common;

namespace Enemies
{
    public sealed class EnemyFactory
    {
        private const int POOL_SIZE = 30;

        private readonly List<Enemy> _cachedEnemies = new(POOL_SIZE);
        private Pool<Enemy> _pool;

        public List<Enemy> CachedEnemies => _cachedEnemies;
        
        public EnemyFactory(Enemy prefab)
        {
            CreatePool(prefab);
        }

        private void CreatePool(Enemy prefab)
            => _pool = new Pool<Enemy>(prefab, POOL_SIZE);

        public Enemy Create()
        {
            var enemy = _pool.Spawn();
            _cachedEnemies.Add(enemy);

            return enemy;
        }

        public void Remove(Enemy bullet)
        {
            if (_cachedEnemies.Contains(bullet))
            {
                _pool.DeSpawn(bullet);
                _cachedEnemies.Remove(bullet);
            }
            else
                throw new ArgumentException($"you're trying to remove {bullet} twice");
        }
    }
}
using System;
using System.Collections.Generic;
using Common;

namespace Bullets
{
    public class BulletFactory
    {
        private const int POOL_SIZE = 50;

        private readonly List<Bullet> _cachedBullets = new(POOL_SIZE);
        private Pool<Bullet> _pool;
        public IReadOnlyList<Bullet> CachedBullets => _cachedBullets;

        public BulletFactory(Bullet prefab)
        {
            CreatePool(prefab);
        }
        
        private void CreatePool(Bullet prefab)
            => _pool = new Pool<Bullet>(prefab, POOL_SIZE);

        public Bullet Create(ProjectileArgs projectileArgs)
        {
            var bullet = _pool.Spawn();
            _cachedBullets.Add(bullet);

            return Build(bullet, projectileArgs);
        }

        public void Remove(Bullet bullet)
        {
            if (_cachedBullets.Contains(bullet))
            {
                _pool.DeSpawn(bullet);
                _cachedBullets.Remove(bullet);
            }
            else
                throw new ArgumentException($"you're trying to remove {bullet} twice");
        }

        private Bullet Build(Bullet bullet, ProjectileArgs projectileArgs)
        {
            bullet.SetPosition(projectileArgs.Position);
            bullet.SetVelocity(projectileArgs.Velocity);
            bullet.SetColor(projectileArgs.Color);
            bullet.SetPhysicsLayer(projectileArgs.PhysicsLayer);
            bullet.SetDamage(projectileArgs.Damage);
            bullet.SetIsPlayer(projectileArgs.IsPlayer);
            return bullet;
        }
    }
}
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

        public BulletFactory(Bullet prefab)
        {
            CreatePool(prefab);
        }

        public List<Bullet> CachedBullets => _cachedBullets;

        private void CreatePool(Bullet prefab)
            => _pool = new Pool<Bullet>(prefab, POOL_SIZE);

        public Bullet Create(Args args)
        {
            var bullet = _pool.Spawn();
            _cachedBullets.Add(bullet);

            return Build(bullet, args);
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

        private Bullet Build(Bullet bullet, Args args)
        {
            return new BulletBuilder(bullet)
                .SetPosition(args.Position)
                .SetColor(args.Color)
                .SetPhysicsLayer(args.PhysicsLayer)
                .SetDamage(args.Damage)
                .SetSpeed(args.Speed)
                .SetIsPlayer(args.IsPlayer)
                .Build();
        }
    }
}
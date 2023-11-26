using Common.Interfaces;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour, IGameStart , IGamePause, IGameResume
    {
        [SerializeField] private Bullet _prefab;
        
        private BulletFactory _bulletFactory;

        public void OnStart()
        {
            _bulletFactory = new BulletFactory(_prefab);
        }
        
        public void OnPause()
        {
            for (var index = 0; index < _bulletFactory.CachedBullets.Count; index++)
                _bulletFactory.CachedBullets[index].SetSimulatePhysics(false);
        }

        public void OnResume()
        {
            for (var index = 0; index < _bulletFactory.CachedBullets.Count; index++)
                _bulletFactory.CachedBullets[index].SetSimulatePhysics(true);
        }
        
        public void Fire(BulletConfig config, Vector3 startPosition, Vector2 direction)
        {
            var args = SetArgs(config, startPosition, direction);

            var bullet = _bulletFactory.Create(args);
            bullet.OnRemoveBullet += OnRemoveBulletEvent;
        }

        private void OnRemoveBulletEvent(Bullet bullet)
        {
            bullet.OnRemoveBullet -= OnRemoveBulletEvent;
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet) 
            => _bulletFactory.Remove(bullet);

        private ProjectileArgs SetArgs(BulletConfig config, Vector2 position, Vector2 direction)
        {
            var args = new ProjectileArgs(
                position,
                direction,
                config.Color,
                (int)config.PhysicsLayer,
                config.Damage,
                config.IsPlayer);
            return args;
        }
    }
}
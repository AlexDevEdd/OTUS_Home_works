using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private Bullet _prefab;
        
        private BulletFactory _bulletFactory;

        private void Awake()
        {
            _bulletFactory = new BulletFactory(_prefab);
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
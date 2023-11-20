using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private Bullet _prefab;
        [SerializeField] private BulletConfigs _bulletConfigs;

        private BulletFactory _bulletFactory;

        private void Awake()
        {
            _bulletFactory = new BulletFactory(_prefab);
        }
        
        public void Fire(BulletType type, Vector3 startPosition, Vector2 direction)
        {
            var args = SetArgs(type, startPosition, direction);

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

        private ProjectileArgs SetArgs(BulletType type, Vector2 position, Vector2 direction)
        {
            var config = _bulletConfigs.GetConfigByType(type);

            var args = new ProjectileArgs(
                position,
                direction,
                config.Color,
                (int)config.PhysicsLayer,
                config.Damage);
            return args;
        }
    }
}
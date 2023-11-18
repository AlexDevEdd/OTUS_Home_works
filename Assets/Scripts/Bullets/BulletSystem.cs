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

        private void FixedUpdate()
        {
            for (int i = 0; i < _bulletFactory.CachedBullets.Count; i++)
                _bulletFactory.CachedBullets[i].UpdatePhysics(Time.fixedDeltaTime);
        }

        public void Fire(BulletType type, Vector2 position, Quaternion rotation)
        {
            var args = SetArgs(type, position, rotation);
            
           var bullet = _bulletFactory.Create(args);
           bullet.OnCollisionEntered += OnBulletCollision;
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
                 damageable.ApplyDamage(bullet.Damage);
            
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= OnBulletCollision;
            _bulletFactory.Remove(bullet);
        }
        
        private Args SetArgs(BulletType type, Vector2 position, Quaternion rotation)
        {
            var config = _bulletConfigs.GetConfigByType(type);
            
            var args = new ArgsBuilder()
                .SetPosition(position)
                .SetVelocity(rotation, config.Speed)
                .SetColor(config.Color)
                .SetPhysicsLayer(config.PhysicsLayer)
                .SetDamage(config.Damage)
                .SetSpeed(config.Speed)
                .SetIsPlayer(config.IsPlayer)
                .Build();
            return args;
        }
    }
}
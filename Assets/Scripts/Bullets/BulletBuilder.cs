using UnityEngine;

namespace Bullets
{
    public class BulletBuilder
    {
        private readonly Bullet _bullet;
        
        public BulletBuilder(Bullet bullet)
        {
            _bullet = bullet;
        }
        
        public BulletBuilder SetPosition(Vector2 position)
        {
            _bullet.SetPosition(position);
            return this;
        }
        
        public BulletBuilder SetColor(Color color)
        {
            _bullet.SetColor(color);
            return this;
        }
        
        public BulletBuilder SetPhysicsLayer(int layer)
        {
            _bullet.SetPhysicsLayer(layer);
            return this;
        }
        
        public BulletBuilder SetDamage(int damage)
        {
            _bullet.SetDamage(damage);
            return this;
        }

        public BulletBuilder SetSpeed(float speed)
        {
            _bullet.SetSpeed(speed);
            return this;
        }
        
        public BulletBuilder SetIsPlayer(bool isPlayer)
        {
            _bullet.SetIsPlayer(isPlayer);
            return this;
        }
        
        public Bullet Build() 
            => _bullet;
    }
}
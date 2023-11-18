using UnityEngine;

namespace Enemies
{
    public class EnemyBuilder
    {
        private readonly Enemy _enemy;
        
        public EnemyBuilder(Enemy enemy)
        {
            _enemy = enemy;
        }
        
        public EnemyBuilder SetPosition(Vector2 position)
        {
            _enemy.SetPosition(position);
            return this;
        }
        
        public EnemyBuilder SetTarget(Transform target)
        {
            _enemy.SetTarget(target);
            return this;
        }
        
        public EnemyBuilder SetSpeed(float speed)
        {
            _enemy.SetSpeed(speed);
            return this;
        }
        
        public EnemyBuilder SetHealth(float health)
        {
            _enemy.SetHealth(health);
            return this;
        }
        
        public Enemy Build() 
            => _enemy;
        
    }
}
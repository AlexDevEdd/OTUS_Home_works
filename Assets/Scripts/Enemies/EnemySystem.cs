using Bullets;
using Character;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public sealed class EnemySystem : MonoBehaviour
    {
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private EnemyConfig _config;
        
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;
        
        private EnemyFactory _factory;

        private void Awake()
        {
            _factory = new EnemyFactory(_prefab);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _factory.CachedEnemies.Count; i++)
                _factory.CachedEnemies[i].UpdatePhysics(Time.fixedDeltaTime);
        }

        public Enemy Create()
        {
            var randomSpawnPos = GetRandomTransform(_spawnPositions);
            var randomAttackPos = GetRandomTransform(_attackPositions);
            var enemy = Build(_factory.Create(), randomSpawnPos.position, randomAttackPos.position);
            enemy.OnDied += OnDied; 
            enemy.OnFire += OnFire; 
            
            return enemy;
        }

        private void OnFire(Transform startPoint) 
            => bulletSystem.Fire( BulletType.Enemy, startPoint.position, startPoint.rotation);

        private void OnDied(Enemy enemy)
        {
            enemy.OnDied -= OnDied; 
            enemy.OnFire -= OnFire;
            _factory.Remove(enemy);
        }

        private Enemy Build(Enemy enemy, Vector2 spawnPos, Vector2 attackPos)
        {
            return new EnemyBuilder(enemy)
                .SetPosition(spawnPos)
                .SetTarget(_player.transform)
                .SetSpeed(_config.Speed)
                .SetHealth(_config.Health)
                .Build();
        }
        
        private Transform GetRandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _factory.CachedEnemies.Count; i++)
            {
                _factory.CachedEnemies[i].OnDied -= OnDied;
                _factory.CachedEnemies[i].OnFire -= OnFire;
            } 
        }
    }
}
﻿using Bullets;
using Character;
using Common.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public sealed class EnemySystem : MonoBehaviour, IFixedTick , IGameStart, IGamePause, IGameResume, IGameFinish
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private EnemyConfig _config;
        
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private Transform[] _attackPositions;
        
        private EnemyFactory _factory;

        public void OnStart() 
            => _factory = new EnemyFactory(_prefab);

        public void FixedTick(float fixedDelta)
        {
            for (int i = 0; i < _factory.CachedEnemies.Count; i++)
                _factory.CachedEnemies[i].UpdatePhysics(Time.fixedDeltaTime);
        }
        
        public void Create()
        {
            var randomSpawnPos = GetRandomTransform(_spawnPositions);
            var randomAttackPos = GetRandomTransform(_attackPositions);
            var enemy = Build(_factory.Create(), randomSpawnPos.position, randomAttackPos.position);
            enemy.Construct(_bulletSystem);
            enemy.Enable();
            enemy.OnDied += OnDied;
        }
        
        private void OnDied(Enemy enemy)
        {
            enemy.OnDied -= OnDied;
            enemy.Disable();
            _factory.Remove(enemy);
        }

        private Enemy Build(Enemy enemy, Vector2 spawnPos, Vector3 attackPos)
        { 
            enemy.SetPosition(spawnPos);
            enemy.SetAttackPosition(attackPos);
            enemy.SetAttackTarget(_player.transform.position);
            enemy.SetHealth(_config.Health);
            return enemy;
        }
        
        private Transform GetRandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
        
        public void OnFinish()
        {
            for (int i = 0; i < _factory.CachedEnemies.Count; i++) 
                _factory.CachedEnemies[i].OnDied -= OnDied;
        }
        
        public void OnPause()
        {
            for (int i = 0; i < _factory.CachedEnemies.Count; i++)
                 _factory.CachedEnemies[i].Disable();
        }

        public void OnResume()
        {
            for (int i = 0; i < _factory.CachedEnemies.Count; i++)
                _factory.CachedEnemies[i].Enable();
        } 
    }
}
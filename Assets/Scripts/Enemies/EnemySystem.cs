using Bullets;
using Character;
using Common.Interfaces;
using GameCore.Installers.ScriptableObjects;
using Systems.Points;
using Systems.Points.Views;
using UnityEngine;
using PrefabProvider = GameCore.Installers.ScriptableObjects.PrefabProvider;
using Random = UnityEngine.Random;

namespace Enemies
{
    public sealed class EnemySystem : IFixedTick, IGameStart, IGamePause, IGameResume, IGameFinish
    {
        private const string ENEMY_KEY = "enemy";

        private readonly Player _player;
        private readonly BulletSystem _bulletSystem;
        private readonly PointSystem _pointSystem;

        private readonly EnemyConfig _config;

        private PointView[] _spawnPositions;
        private PointView[] _attackPositions;

        private readonly EnemyFactory _factory;

        public EnemySystem(Player player, BulletSystem bulletSystem, PrefabProvider prefabProvider,
            PointSystem pointSystem, GameBalance balance)
        {
            _player = player;
            _bulletSystem = bulletSystem;
            _pointSystem = pointSystem;
            _config = balance.EnemyConfig;

            var _prefab = prefabProvider.GetPrefab<Enemy>(ENEMY_KEY);
            _factory = new EnemyFactory(_prefab);
        }

        public void OnStart()
        {
            _spawnPositions = _pointSystem.GetPointsByType<PointView>(PointType.EnemySpawnPoint);
            _attackPositions = _pointSystem.GetPointsByType<PointView>(PointType.AttackPoint);
        }

        public void FixedTick(float fixedDelta)
        {
            for (int i = 0; i < _factory.CachedEnemies.Count; i++)
                _factory.CachedEnemies[i].UpdatePhysics(Time.fixedDeltaTime);
        }

        public void Create()
        {
            var randomSpawnPos = GetRandomTransform(_spawnPositions);
            var randomAttackPos = GetRandomTransform(_attackPositions);
            var enemy = _factory.Create();
            enemy.Construct(_bulletSystem);
            Build(enemy, randomSpawnPos.position, randomAttackPos.position);
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

        private Transform GetRandomTransform(PointView[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index].Point;
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
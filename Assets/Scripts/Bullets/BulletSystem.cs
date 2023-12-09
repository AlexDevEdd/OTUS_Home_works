using Common.Interfaces;
using GameCore.Installers.ScriptableObjects;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem :  IGamePause, IGameResume
    {
        private readonly BulletFactory _bulletFactory;
        private readonly GameBalance _balance;
        
        public BulletSystem(BulletFactory bulletFactory, GameBalance balance)
        {
            _bulletFactory = bulletFactory;
            _balance = balance;
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
        
        public void Fire(TeamType type, Vector3 startPosition, Vector2 direction)
        {
            var config = _balance.BulletConfigs.GetConfigByType(type);
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
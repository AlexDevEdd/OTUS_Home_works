using Common.Interfaces;
using DG.Tweening;

namespace Enemies
{
    public sealed class EnemySpawner : IGameStart, IGamePause, IGameResume, IGameFinish
    {
        private const int LOOP = -1;
        private float _spawnDelay = 1f;
        
        private readonly EnemySystem _enemySystem;
        private Tween _tween;

        public EnemySpawner(EnemySystem enemySystem)
        {
            _enemySystem = enemySystem;
        }

        private void Spawn()
            => _enemySystem.Create();
        
        public void OnStart()
        {
            _tween = DOVirtual.DelayedCall(_spawnDelay, Spawn)
                .SetLoops(LOOP);
        }

        public void OnPause() 
            => _tween.Pause();

        public void OnResume() 
            => _tween.Play();

        public void OnFinish()
            => _tween?.Kill();
    }
}
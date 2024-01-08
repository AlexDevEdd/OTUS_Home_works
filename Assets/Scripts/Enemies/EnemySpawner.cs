using Common.Interfaces;
using DG.Tweening;
using Zenject;

namespace Enemies
{
    public sealed class EnemySpawner : IGameStart, IGamePause, IGameResume, IGameFinish
    {
        private const int LOOP = -1;
        private const float SPAWN_DELAY = 1f;

        private readonly EnemySystem _enemySystem;
        private Tween _tween;

        [Inject]
        public EnemySpawner(EnemySystem enemySystem)
        {
            _enemySystem = enemySystem;
        }

        private void Spawn()
            => _enemySystem.Create();
        
        public void OnStart()
        {
            _tween = DOVirtual.DelayedCall(SPAWN_DELAY, Spawn)
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
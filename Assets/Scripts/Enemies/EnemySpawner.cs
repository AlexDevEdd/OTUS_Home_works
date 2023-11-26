using Common.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemySpawner : MonoBehaviour, IGameStart, IGamePause, IGameResume, IGameFinish
    {
        private const int LOOP = -1;

        [SerializeField] private float _spawnDelay = 1f;
        [SerializeField] private EnemySystem _enemySystem;

        private Tween _tween;
        
        private void Spawn()
            => _enemySystem.Create();

        private void OnDisable()
            => _tween?.Kill();

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
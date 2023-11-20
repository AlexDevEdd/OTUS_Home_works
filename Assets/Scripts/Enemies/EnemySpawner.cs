using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        private const int LOOP = -1;

        [SerializeField] private float _spawnDelay = 1f;
        [SerializeField] private EnemySystem _enemySystem;

        private Tween _tween;

        private void Start() =>
            _tween = DOVirtual.DelayedCall(_spawnDelay, Spawn)
                .SetLoops(LOOP);

        private void Spawn()
            => _enemySystem.Create();

        private void OnDisable()
            => _tween?.Kill();
    }
}
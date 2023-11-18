using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        private const float SPAWN_DELAY = 1f;
        private const int LOOP = -1;
        
        [SerializeField] private EnemySystem _enemySystem;
        
        private Tween _tween;
        
        private void Start()
        {
            _tween = DOVirtual.DelayedCall(SPAWN_DELAY, (() =>
            {
                _enemySystem.Create();
            })).SetLoops(LOOP);
        }
        
        private void OnDisable() 
            => _tween?.Kill();
    }
}
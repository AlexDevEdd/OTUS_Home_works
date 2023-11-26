using Character;
using Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Components
{
    public sealed class GameOverObserver : MonoBehaviour , IGameStart
    {
        [SerializeField] private Player _player;

        public void OnStart() 
            => _player.OnDied += OnGameOver;

        private void OnGameOver()
        {
            _player.OnDied -= OnGameOver;
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
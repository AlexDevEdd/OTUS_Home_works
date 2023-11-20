using Character;
using UnityEditor;
using UnityEngine;

namespace Components
{
    public sealed class GameOverObserver : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private void Awake()
        {
            _player.OnDied += OnGameOver;
        }

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
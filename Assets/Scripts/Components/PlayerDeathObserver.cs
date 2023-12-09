using Character;
using Common.Interfaces;
using GameCore;
using UnityEditor;

namespace Components
{
    public sealed class PlayerDeathObserver : IGameStart
    {
        private readonly GameManager _gameManager;
        private readonly Player _player;
        
        public PlayerDeathObserver(Player player, GameManager gameManager)
        {
            _player = player;
            _gameManager = gameManager;
        }

        public void OnStart() 
            => _player.OnDied += OnGameOver;

        private void OnGameOver()
        {
            _player.OnDied -= OnGameOver;
            _gameManager.OnFinish();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
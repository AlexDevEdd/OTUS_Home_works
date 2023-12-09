using Common.Interfaces;
using UI;
using UnityEngine;

namespace GameCore
{
    public class GameCoolDownLauncher : MonoBehaviour, IGameFinish
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private StartGameTimer _gameStartTimer;
        
        private void Start()
        {
            _gameStartTimer.OnStartTimerCompleted += OnStartTimerCompleted;
            _gameStartTimer.StartTimer();
        }

        private void OnStartTimerCompleted()
        {
            _gameStartTimer.OnStartTimerCompleted -= OnStartTimerCompleted;
            _gameManager.OnStartEvent();
        }
        
        public void OnFinish() 
            => _gameStartTimer.OnStartTimerCompleted -= OnStartTimerCompleted;
    }
}
using UI;
using UnityEngine;
using Zenject;

namespace GameCore
{
    public class GameCoolDownLauncher : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
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
    }
}
using Common.Interfaces;
using Systems.InputSystem;
using Zenject;

namespace GameCore
{
    public class PauseResumeInputListener : IGameStart, IGameFinish
    {
        private readonly IPauseResumeInput _input;
        private readonly GameManager _gameManager;

        [Inject]
        public PauseResumeInputListener(IPauseResumeInput input, GameManager gameManager)
        {
            _input = input;
            _gameManager = gameManager;
        }

        public void OnStart()
        {
            _input.OnPauseEvent += OnPauseEvent;
            _input.OnResumeEvent += OnResumeEvent;
        }
        
        public void OnFinish()
        {
            _input.OnPauseEvent -= OnPauseEvent;
            _input.OnResumeEvent -= OnResumeEvent;
        }

        private void OnResumeEvent() 
            => _gameManager.OnResumeEvent();

        private void OnPauseEvent() 
            => _gameManager.OnPauseEvent();
    }
}
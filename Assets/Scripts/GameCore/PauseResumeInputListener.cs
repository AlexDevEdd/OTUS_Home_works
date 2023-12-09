using Common.Interfaces;
using Systems.InputSystem;

namespace GameCore
{
    public class PauseResumeInputListener : IGameStart, IGameFinish
    {
        private readonly IPauseResumeInput _input;
        private readonly GameManager _gameManager;

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
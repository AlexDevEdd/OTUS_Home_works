using Common.Interfaces;
using Input;
using UnityEngine;


namespace GameCore
{
    public class PauseResumeInputListener : MonoBehaviour, IGameStart, IGameFinish
    {
        [SerializeField] private InputListener _input;
        [SerializeField] private GameManager _gameManager;

        public void OnStart()
        {
            _input.OnPause += OnPauseEvent;
            _input.OnResume += OnResumeEvent;
        }
        
        public void OnFinish()
        {
            _input.OnPause -= OnPauseEvent;
            _input.OnResume -= OnResumeEvent;
        }

        private void OnResumeEvent() 
            => _gameManager.OnResumeEvent();

        private void OnPauseEvent() 
            => _gameManager.OnPauseEvent();

       
    }
}
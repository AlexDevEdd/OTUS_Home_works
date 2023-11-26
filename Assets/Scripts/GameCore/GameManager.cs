using System.Linq;
using Common.Interfaces;
using Input;
using UI;
using UnityEngine;

namespace GameCore
{
    public class GameManager : MonoBehaviour 
    {
        [SerializeField] private GameState _gameState;
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private StartGameTimer _startGameTimer;
        
        private IGameStart[] _startListeners;
        private IGamePause[] _pauseListener;
        private IGameResume[] _resumeListeners;
        private IGameFinish[] _finishListeners;
        private ITick[] _updates;
        private IFixedTick[] _fixedUpdates;

        public void Init(IGameListener[] listeners)
        {
            _startListeners = listeners.OfType<IGameStart>().ToArray();
            _pauseListener = listeners.OfType<IGamePause>().ToArray();
            _resumeListeners = listeners.OfType<IGameResume>().ToArray();
            _finishListeners = listeners.OfType<IGameFinish>().ToArray();
            _updates = listeners.OfType<ITick>().ToArray();
            _fixedUpdates = listeners.OfType<IFixedTick>().ToArray();
            
            _inputListener.OnPause += OnPauseEvent;
            _inputListener.OnResume += OnResumeEvent;
        }

        private void Start()
        {
            _startGameTimer.StartTimer();
            _startGameTimer.OnStartGame += OnStartGameEvent;
        }

        private void OnStartGameEvent()
        {
            _startGameTimer.OnStartGame -= OnStartGameEvent;
            OnStartEvent();
        }

        private void OnStartEvent()
        {
            ChangeState(GameState.Start);
            
            for (int i = 0; i < _startListeners.Length; i++) 
                _startListeners[i].OnStart();
            
            ChangeState(GameState.GameLoop);
        }
        
        private void OnPauseEvent()
        {
            ChangeState(GameState.Pause);
            
            for (int i = 0; i < _pauseListener.Length; i++) 
                _pauseListener[i].OnPause();
        }

        private void OnResumeEvent()
        {
            ChangeState(GameState.GameLoop);
            
            for (int i = 0; i < _resumeListeners.Length; i++) 
                _resumeListeners[i].OnResume();
        }

        private void Update()
        {
            if(IsPaused()) return;
            for (int i = 0; i < _updates.Length; i++) 
                _updates[i].Tick(Time.deltaTime);
        }
        
        private void FixedUpdate()
        {
            if(IsPaused()) return;
            for (int i = 0; i < _fixedUpdates.Length; i++) 
                _fixedUpdates[i].FixedTick(Time.fixedDeltaTime);
        }
        
        private void OnFinish()
        {
            ChangeState(GameState.Finish);
            foreach (var listener in _finishListeners) 
                listener.OnFinish();

        }

        private bool IsPaused() 
            => _gameState != GameState.GameLoop;

        private void ChangeState(GameState state)
        {
            if(Equals(_gameState, state)) return;
            Debug.Log(state);
            _gameState = state;
        }

        private void OnDestroy() 
            => OnFinish();
    }
}
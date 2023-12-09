﻿using System.Linq;
using Common.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore
{
    public class GameManager : MonoBehaviour 
    {
        [SerializeField, ReadOnly] private GameState _gameState;
        
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
        }
        
        public void OnStartEvent()
        {
            if(_gameState != GameState.None) 
                return;
            
            ChangeState(GameState.Start);
            
            for (int i = 0; i < _startListeners.Length; i++) 
                _startListeners[i].OnStart();
            
            ChangeState(GameState.GameLoop);
        }
        
        public void OnPauseEvent()
        {
            if(_gameState != GameState.GameLoop) 
                return;
            
            ChangeState(GameState.Pause);
            
            for (int i = 0; i < _pauseListener.Length; i++) 
                _pauseListener[i].OnPause();
        }

        public void OnResumeEvent()
        {
            if(_gameState != GameState.Pause) 
                return;
            
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
        
        public void OnFinish()
        {
            if(_gameState is GameState.Finish or GameState.None or GameState.Start) 
                return;
            
            ChangeState(GameState.Finish);
            foreach (var listener in _finishListeners) 
                listener.OnFinish();

        }

        private bool IsPaused() 
            => _gameState != GameState.GameLoop;

        private void ChangeState(GameState state)
        {
            if(Equals(_gameState, state)) return;
            _gameState = state;
        }

        private void OnDestroy() 
            => OnFinish();
    }
}
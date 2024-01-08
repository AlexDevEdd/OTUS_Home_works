using System;
using Common.Interfaces;
using Scripts.Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Systems.InputSystem
{
    public class InputReader : GameInputMap.IGamePlayActions, GameInputMap.IUIActions,
        IInput, IGameStart, IGamePause, IGameResume
    {
        public event Action<Vector2> OnMoveEvent;
        public event Action OnFireEvent;
        public event Action OnPauseEvent;
        public event Action OnResumeEvent;
        
        private readonly GameInputMap _input;

        [Inject]
        public InputReader()
        {
            _input = new GameInputMap();
            _input.GamePlay.SetCallbacks(this);
            _input.UI.SetCallbacks(this);
        }
        
        public void OnStart() 
            => SetGamePlay();

        public void OnPause() 
            => SetUI();

        public void OnResume() 
            => SetGamePlay();
        
        public void OnMove(InputAction.CallbackContext context)
        {
            OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                OnPauseEvent?.Invoke();
                SetUI();
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed) 
                OnFireEvent?.Invoke();
        }

        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                OnResumeEvent?.Invoke();
                SetGamePlay();
            }
        }
        
        private void SetGamePlay()
        {
            _input.UI.Disable();
            _input.GamePlay.Enable();
        }

        private void SetUI()
        {
            _input.GamePlay.Disable();
            _input.UI.Enable();
        }
    }
}
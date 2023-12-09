using System;
using Common.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.InputSystem
{
    public class InputReader : GameInput.IGamePlayActions, GameInput.IUIActions,
        IInput, IGameStart, IGamePause, IGameResume
    {
        public event Action<Vector2> OnMoveEvent;
        public event Action OnFireEvent;
        public event Action OnPauseEvent;
        public event Action OnResumeEvent;
        
        private readonly GameInput _input;

        public InputReader()
        {
            _input = new GameInput();
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
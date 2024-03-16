using System;
using _Project.Scripts.UI.ButtonComponents.States;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts.UI.ButtonComponents.Buttons
{
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [Space] 
        [SerializeField] protected Image ButtonBackground;
        [SerializeField] protected Sprite AvailableButtonSprite;
        [SerializeField] protected Sprite LockedButtonSprite;
        
        [Space] 
        [SerializeField] protected ButtonStateType buttonStateType;

        protected readonly ButtonStateController StateController = new();
        public Button Button => _button;

        private void Awake()
        {
            SetUpStates();
        }

        protected abstract void SetUpStates();
        
        public void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void RemoveListener(UnityAction action)
        {
            _button.onClick.RemoveListener(action);
        }
        
        public void SetAvailable(bool isAvailable)
        {
            var state = isAvailable ? ButtonStateType.AVAILABLE : ButtonStateType.LOCKED;
            SetState(state);
        }

        public void SetState(ButtonStateType buttonStateType)
        {
            this.buttonStateType = buttonStateType;
            StateController.SetState(this.buttonStateType);
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            try
            {
                SetState(buttonStateType);
            }
            catch (Exception)
            {
                // ignored
            }
        }
#endif
    }
}
using _Project.Scripts.UI.ButtonComponents.States;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.ButtonComponents.Buttons
{
    public class SimpleButton : BaseButton
    {
        [Space]
        [SerializeField] private TextMeshProUGUI _buttonText;
        
        protected override void SetUpStates()
        {
            StateController.AddState(ButtonStateType.AVAILABLE,
                new AvailableButtonState(Button, ButtonBackground, AvailableButtonSprite));
            
            StateController.AddState(ButtonStateType.LOCKED,
                new LockedButtonState(Button, ButtonBackground, LockedButtonSprite));
        }
        
        public void SetButtonText(string text)
        {
            _buttonText.text = text;
        }
    }
}
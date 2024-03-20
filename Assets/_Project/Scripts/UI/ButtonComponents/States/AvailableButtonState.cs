using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.ButtonComponents.States
{
    public sealed class AvailableButtonState : BaseButtonState
    {
        public AvailableButtonState(Button button, Image buttonBackground, Sprite backgroundSprite) 
            : base(button, buttonBackground, backgroundSprite) {}
        
        public override void SetState()
        {
            Button.interactable = true;
            ButtonBackground.sprite = BackgroundSprite;
        }
    }
}
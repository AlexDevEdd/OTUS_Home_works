using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.ButtonComponents.States
{
    public sealed class AvailableBuyButtonState : BaseButtonState
    {
        private readonly ButtonObjects _buttonObjects;
        
        public AvailableBuyButtonState(Button button, Image buttonBackground, Sprite backgroundSprite,
            ButtonObjects buttonObjects) : base(button, buttonBackground, backgroundSprite)
        {
            _buttonObjects = buttonObjects;
        }
        
        public override void SetState()
        {
            Button.interactable = true;
            ButtonBackground.sprite = BackgroundSprite;
            
            _buttonObjects.PriceContainer.SetActive(true);
            _buttonObjects.TitleTextGO.SetActive(true);
            _buttonObjects.MaxTextGO.SetActive(false);
        }
    }
}
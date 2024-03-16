using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.ButtonComponents.States
{
    public sealed class MaxBuyButtonState : BaseButtonState
    {
        private readonly ButtonObjects _buttonObjects;
        
        public MaxBuyButtonState(Button button, Image buttonBackground, Sprite backgroundSprite,
            ButtonObjects buttonObjects) : base(button, buttonBackground, backgroundSprite)
        {
            _buttonObjects = buttonObjects;
        }
        
        public override void SetState()
        {
            Button.interactable = true;
            ButtonBackground.sprite = BackgroundSprite;
            
            _buttonObjects.PriceContainer.SetActive(false);
            _buttonObjects.TitleTextGO.SetActive(false);
            _buttonObjects.MaxTextGO.SetActive(true);
        }
    }
}
using _Project.Scripts.UI.ButtonComponents.States;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.ButtonComponents.Buttons
{
    public sealed class BuyButton : BaseButton
    {
        [SerializeField] private Sprite _maxButtonSprite;
        
        [Space]
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _titleText;

        [Space] 
        [SerializeField] private ButtonObjects _buttonObjects;
        
        protected override void SetUpStates()
        {
            StateController.AddState(ButtonStateType.AVAILABLE,
                new AvailableBuyButtonState(Button, ButtonBackground, AvailableButtonSprite, _buttonObjects));
            
            StateController.AddState(ButtonStateType.LOCKED,
                new LockedBuyButtonState(Button, ButtonBackground, LockedButtonSprite, _buttonObjects));
            
            StateController.AddState(ButtonStateType.MAX,
                new MaxBuyButtonState(Button, ButtonBackground, _maxButtonSprite, _buttonObjects));
        }
        
        public void SetPrice(string price)
        {
            _priceText.text = price;
        }
        
        public void SetTitle(string title)
        {
            _titleText.text = title;
        }
    }
}
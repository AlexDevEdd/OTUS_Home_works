using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.ButtonComponents.States
{
    public abstract class BaseButtonState
    {
        protected readonly Button Button;
        protected readonly Image ButtonBackground;
        protected readonly Sprite BackgroundSprite;

        protected BaseButtonState(Button button, Image buttonBackground, Sprite backgroundSprite)
        {
            ButtonBackground = buttonBackground;
            Button = button;
            BackgroundSprite = backgroundSprite;
        }

        public abstract void SetState();
    }
}
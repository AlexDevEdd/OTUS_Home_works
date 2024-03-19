using _Game.Scripts.Tools;
using _Project.Scripts.UI.ButtonComponents.Buttons;
using _Project.Scripts.UI.Windows;
using UnityEngine;

namespace _Project.Scripts.UI.Upgrades
{
    public class UpgradePopUpView : MonoBehaviour, IWindow
    {
        [SerializeField] private Transform _upgradesContainer;
        [SerializeField] private CloseButton _closeButton;
        
        public void Show()
        {
            _closeButton.AddListener(Hide);
            gameObject.Activate();
        }

        public void Hide()
        {
            _closeButton.RemoveListener(Hide);
            gameObject.Deactivate();
        }
    }
}
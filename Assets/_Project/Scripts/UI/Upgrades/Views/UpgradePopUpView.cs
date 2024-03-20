using _Game.Scripts.Tools;
using _Project.Scripts.UI.ButtonComponents.Buttons;
using _Project.Scripts.UI.Upgrades.Presenters;
using UnityEngine;

namespace _Project.Scripts.UI.Upgrades.Views
{
    public class UpgradePopUpView : MonoBehaviour
    {
        [SerializeField] private CloseButton _closeButton;
        
        private IUpgradePopUpPresenter _popUpPresenter;
        public void Show(IUpgradePopUpPresenter popUpPresenter)
        {
            gameObject.Activate();
            _popUpPresenter = popUpPresenter;
            _popUpPresenter.Show();
            _popUpPresenter.Subscribe();
            Subscribes();
        }
        
        public void Hide()
        {
            _popUpPresenter.CloseCommand.Execute();
            _closeButton.RemoveListener(Hide);
            gameObject.Deactivate();
        }
        
        private void Subscribes()
        {
            _closeButton.AddListener(Hide);
        }
    }
}
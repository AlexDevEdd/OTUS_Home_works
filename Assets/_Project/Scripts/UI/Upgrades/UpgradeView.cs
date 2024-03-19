using _Game.Scripts.Tools;
using _Project.Scripts.UI.ButtonComponents.Buttons;
using _Project.Scripts.UI.ButtonComponents.States;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Upgrades
{
    public sealed class UpgradeView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private TMP_Text _levelText;
        [Space][Header("Button")]
        [SerializeField] private BuyButton _buyButton;
        
        private IUpgradePresenter _upgradePresenter;
        
        public void Show(IUpgradePresenter upgradePresenter)
        {
            _upgradePresenter = upgradePresenter;
            _icon.sprite = _upgradePresenter.Icon;
            _nameText.text = _upgradePresenter.Name;
            _valueText.text = upgradePresenter.GetConvertedValueText();
            _levelText.text = upgradePresenter.GetConvertedLevelText();
            
            _upgradePresenter.Subscribe();
            Subscribes();
            UpdateButtonState(default);
        }
      
        public void Hide()
        {
            _upgradePresenter.UnSubscribe();
        }
        
        private void Subscribes()
        {
            _upgradePresenter.Level
                .Subscribe(OnLevelChanged)
                .AddTo(_upgradePresenter.CompositeDisposable);
            
            _upgradePresenter.Value
                .Subscribe(OnValueChanged)
                .AddTo(_upgradePresenter.CompositeDisposable);
            
            _upgradePresenter.UpgradeCommand
                .BindTo(_buyButton.Button)
                .AddTo(_upgradePresenter.CompositeDisposable); 
                
            _upgradePresenter.UpdateButtonStateCommand
                .Subscribe(UpdateButtonState)
                .AddTo(_upgradePresenter.CompositeDisposable); 
        }
        
        private void OnLevelChanged(int value)
        {
            _levelText.text = _upgradePresenter.GetConvertedLevelText();
        }
        
        private void OnValueChanged(float value)
        {
            _valueText.text = _upgradePresenter.GetConvertedValueText();
        }

        private void UpdateButtonState(Unit _)
        {
            if (_upgradePresenter.IsMaxLevel())
            {
                _buyButton.SetState(ButtonStateType.MAX);
                _upgradePresenter.UnSubscribe();
                return;
            }
            
            _buyButton.SetPrice(_upgradePresenter.GetPrice());
            
            _buyButton.SetState(_upgradePresenter.CanLevelUp()
                ? ButtonStateType.AVAILABLE 
                : ButtonStateType.LOCKED);
        }
    }
}
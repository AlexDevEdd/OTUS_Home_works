using _Project.Scripts.UI.ButtonComponents.Buttons;
using _Project.Scripts.UI.Upgrades;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class DevPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balanceText;
        [SerializeField] private SimpleButton _addMoneyButton;
        [SerializeField] private SimpleButton _showPopUpButton;
        [SerializeField] private SimpleButton _hidePopUpButton;
        
        [Inject] public PlayerUpgradePopUpSystem _playerUpgradePopUpSystem;
        [Inject] public MoneyStorage _moneyStorage;

        private void Awake()
        {
            _addMoneyButton.AddListener(AddMoney);
            _showPopUpButton.AddListener(Show);
            _hidePopUpButton.AddListener(Hide);
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
        }

        private void OnMoneyChanged(int value)
        {
            _balanceText.text = $"Balance: {value}";
        }

        private void AddMoney()
        {
            _moneyStorage.EarnMoney(500);
        }

        private void Show()
        {
            _playerUpgradePopUpSystem.ShowPopUp();
        }

        private void Hide()
        {
            _playerUpgradePopUpSystem.ClosePopUp();
        }

        private void OnDestroy()
        {
            _addMoneyButton.RemoveListener(AddMoney);
            _showPopUpButton.RemoveListener(Show);
            _hidePopUpButton.RemoveListener(Hide);
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        }
    }
}
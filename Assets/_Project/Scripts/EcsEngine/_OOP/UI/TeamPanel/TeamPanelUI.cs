using _Project.Scripts.EcsEngine._OOP.UI.Buttons;
using _Project.Scripts.EcsEngine.Enums;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.UI.TeamPanel
{
    public class TeamPanelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _teamNameText;
        [SerializeField] private SimpleIconButton _meleeButton;
        [SerializeField] private SimpleIconButton _rangeButton;
        [SerializeField] private CloseButton _closeButton;

        private ITeamPanelPresenter _presenter;

        public void Show(ITeamPanelPresenter presenter)
        {
            _presenter = presenter;
            _teamNameText.text = presenter.TeamName;
            _meleeButton.SetButtonIcon(presenter.MeleeIcon);
            _rangeButton.SetButtonIcon(presenter.RangeIcon);
            _presenter.Subscribe();
            
            Subscribes();
            gameObject.SetActive(true);
        }
        
        private void Subscribes()
        {
            _meleeButton.AddListener(MeleeButtonClicked);
            _rangeButton.AddListener(RangeButtonClicked);
            _closeButton.AddListener(Hide);
        }

        private void MeleeButtonClicked()
        {
            _presenter.MeleeSpawnCommand.Execute(UnitType.Melee);
        }
        
        private void RangeButtonClicked()
        {
            _presenter.RangeSpawnCommand.Execute(UnitType.Range);
        }
        
        public void Hide()
        {
            if (isActiveAndEnabled)
            {
                _presenter.CloseCommand.Execute();
                _meleeButton.RemoveListener(MeleeButtonClicked);
                _rangeButton.RemoveListener(RangeButtonClicked);
                _closeButton.RemoveListener(Hide);
                 gameObject.SetActive(false);
            }
        }
    }
}
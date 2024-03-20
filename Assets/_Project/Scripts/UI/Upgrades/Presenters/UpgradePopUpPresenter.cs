using System.Collections.Generic;
using _Project.Scripts.UI.Upgrades.Views;
using Sirenix.Utilities;
using UniRx;

namespace _Project.Scripts.UI.Upgrades.Presenters
{
    public sealed class UpgradePopUpPresenter : IUpgradePopUpPresenter
    {
        private readonly MoneyStorage _moneyStorage;
        private readonly ReactiveCollection<UpgradeView> _upgradeViews;
        private readonly List<IUpgradePresenter> _upgradePresenters;
        public ReactiveCommand CloseCommand { get; }
        public CompositeDisposable CompositeDisposable { get; private set; }
        
        public UpgradePopUpPresenter(PlayerUpgradePopUpSystem playerUpgradePopUpSystem, MoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
            _upgradeViews = new ReactiveCollection<UpgradeView>(playerUpgradePopUpSystem.UpgradeViews);
            _upgradePresenters = new List<IUpgradePresenter>(playerUpgradePopUpSystem.UpgradePresenters);
            
            CloseCommand = new ReactiveCommand();
        }

        public void Show()
        {
            UpdateView();
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
        }

        private void OnMoneyChanged(int obj)
        {
            UpdateButtons();
        }

        private void Hide()
        {
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
            _upgradeViews.ForEach(u => u.Hide());
        }

        private void UpdateView()
        {
            for (var i = 0; i < _upgradePresenters.Count; i++)
            {
                if(i > _upgradeViews.Count) return;
                _upgradeViews[i].Show(_upgradePresenters[i]);
            }
        }

        private void UpdateButtons()
        {
            _upgradePresenters.ForEach(u => u.UpdateButtonState());
        }
        
        public void Subscribe()
        {
            CompositeDisposable = new CompositeDisposable();
            CloseCommand.Subscribe(OnCloseCommand)
                .AddTo(CompositeDisposable);

            _upgradeViews.ObserveAdd()
                .Subscribe(_ => UpdateView())
                .AddTo(CompositeDisposable);
        }
        
        public void UnSubscribe()
        {
            CompositeDisposable?.Dispose();
        }
        
        private void OnCloseCommand(Unit _)
        {
            Hide();
            UnSubscribe();
        }
    }
}
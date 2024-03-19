using System;
using System.Collections.Generic;
using _Game.Scripts.Tools;
using _Project.Scripts.UI.ButtonComponents.Buttons;
using _Project.Scripts.Upgrades;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using PrefabProvider = _Project.Scripts.ScriptableConfigs.PrefabProvider;

namespace _Project.Scripts.UI.Upgrades
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

    [UsedImplicitly]
    public sealed class PlayerUpgradeViewFactory : IInitializable
    {
        private readonly PrefabProvider _prefabProvider;
        private readonly Transform _parent;
        private UpgradeView _prefab;
        
        public PlayerUpgradeViewFactory(PrefabProvider prefabProvider, Transform parent)
        {
            _prefabProvider = prefabProvider;
            _parent = parent;
        }

        public void Initialize()
        {
            _prefab = _prefabProvider.GetPrefab<UpgradeView>("UpgradeView");
        }

        public UpgradeView CreateUpgradeView()
        {
            var view = Object.Instantiate(_prefab);
            view.SetParent(_parent);
            return view;
        }
    }
    
    [UsedImplicitly]
    public sealed class UpgradeUIPresenterFactory
    {
        private readonly MoneyStorage _moneyStorage;
        private readonly PlayerUpgradeSystem _upgradeSystem;

        public UpgradeUIPresenterFactory(MoneyStorage moneyStorage, PlayerUpgradeSystem upgradeSystem)
        {
            _moneyStorage = moneyStorage;
            _upgradeSystem = upgradeSystem;
        }

        public IUpgradePresenter CreateUpgradePresenter(StatType id)
        {
            var upgrade = _upgradeSystem.GetUpgrade(id);
            return new UpgradeUIPresenter(upgrade, _upgradeSystem, _moneyStorage);
        }
    }

    [UsedImplicitly] [Serializable]
    public sealed class PlayerUpgradePopUpSystem : IInitializable
    {
        private readonly UpgradeUIPresenterFactory _upgradeUIPresenterFactory;
        private readonly PlayerUpgradeViewFactory _upgradeViewFactory;
        private readonly PlayerUpgradeSystem _upgradeSystem;
        private readonly UpgradePopUpView _upgradePopUpView;
        private readonly MoneyStorage _moneyStorage;
        
        private IUpgradePopUpPresenter _upgradePopUpPresenter;
        
        public readonly List<UpgradeView> UpgradeViews = new ();
        public readonly List<IUpgradePresenter> UpgradePresenters = new ();
        
        public PlayerUpgradePopUpSystem(PlayerUpgradeViewFactory upgradeViewFactory, PlayerUpgradeSystem upgradeSystem,
            UpgradeUIPresenterFactory upgradeUIPresenterFactory, UpgradePopUpView upgradePopUpView, MoneyStorage moneyStorage)
        {
            _upgradeUIPresenterFactory = upgradeUIPresenterFactory;
            _upgradeViewFactory = upgradeViewFactory;
            _upgradeSystem = upgradeSystem;
            _upgradePopUpView = upgradePopUpView;
            _moneyStorage = moneyStorage;
        }
        
        public void Initialize()
        {
            foreach (var upgrade in _upgradeSystem.GetAllUpgrades())
            {
                UpgradeViews.Add( _upgradeViewFactory.CreateUpgradeView());
                UpgradePresenters.Add( _upgradeUIPresenterFactory.CreateUpgradePresenter(upgrade.Id));
            }

            _upgradePopUpPresenter = new UpgradePopUpPresenter(this, _moneyStorage);
        }

        [Button]
        public void ShowPopUp()
        {
            _upgradePopUpView.Show(_upgradePopUpPresenter);
        }
        
        [Button]
        public void ClosePopUp()
        {
            _upgradePopUpView.Hide();
        }
    }
}
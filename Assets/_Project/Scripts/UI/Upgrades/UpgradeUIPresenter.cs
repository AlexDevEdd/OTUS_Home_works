using _Project.Scripts.Upgrades;
using _Project.Scripts.Upgrades.Stats;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI.Upgrades
{
    public sealed class UpgradeUIPresenter : IUpgradePresenter
    {
        private const string GREEN_COLOR = "#309D1E";
        
        private readonly PlayerUpgradeSystem _upgradeSystem;
        private readonly MoneyStorage _moneyStorage;
        
        private readonly ReactiveProperty<int> _price;
        private readonly ReactiveProperty<int> _level;
        private readonly ReactiveProperty<float> _value;
        private readonly ReactiveProperty<float> _nextValue;
        
        public IReadOnlyReactiveProperty<int> Price => _price;
        public IReadOnlyReactiveProperty<int> Level => _level;
        public IReadOnlyReactiveProperty<float> Value => _value;
        public IReadOnlyReactiveProperty<float> NextValue => _nextValue;
        
        public StatType Id { get; }
        public string Name { get; }
        public int MaxLevel { get; }
        public Sprite Icon { get; }
        
        public ReactiveCommand UpgradeCommand { get; }
        public ReactiveCommand UpdateButtonStateCommand { get; }
        public CompositeDisposable CompositeDisposable { get; private set; }
        
        public UpgradeUIPresenter(Upgrade upgrade, PlayerUpgradeSystem upgradeSystem, MoneyStorage moneyStorage)
        {
            _upgradeSystem = upgradeSystem;
            _moneyStorage = moneyStorage;
            
            Id = upgrade.Id;
            Name = upgrade.StatName;
            MaxLevel = upgrade.MaxLevel;
            Icon = upgrade.Icon;
            
            _price = new ReactiveProperty<int>(upgrade.NextPrice);
            _level = new ReactiveProperty<int>(upgrade.Level);
            _value = new ReactiveProperty<float>(upgrade.GetStatValue(_level.Value));
            _nextValue = new ReactiveProperty<float>(upgrade.GetStatValue(_level.Value + 1));
            
            UpgradeCommand = new ReactiveCommand();
            UpdateButtonStateCommand = new ReactiveCommand();
            CompositeDisposable = new CompositeDisposable();
        }

        public void Subscribe()
        {
            CompositeDisposable = new CompositeDisposable();
            UpgradeCommand.Subscribe(OnUpgradeCommand)
                .AddTo(CompositeDisposable);
            
            UpdateButtonStateCommand.Subscribe()
                .AddTo(CompositeDisposable);
            
            _moneyStorage.OnMoneyEarned += OnMoneyChanged;
        }
        
        private void OnMoneyChanged(int value)
        {
            UpdateButtonStateCommand.Execute();
        }

        public void UnSubscribe()
        {
            CompositeDisposable?.Dispose();
            _moneyStorage.OnMoneyEarned -= OnMoneyChanged;
        }
        
        public bool CanLevelUp()
            => _upgradeSystem.CanLevelUp(Id);

        public bool IsMaxLevel() 
            => _upgradeSystem.IsMaxLevel(Id);

        public string GetPrice()
            => _price.Value.ToString();
        
        public string GetConvertedValueText()
            => $"Value: {GetStatValue()} <color={GREEN_COLOR}>(+{GetNextValue()})</color>";
        
        public string GetConvertedLevelText()
            => $"Value: {GetCurrentLevel()}/{GetMaxLevel()})";
        
        private string GetCurrentLevel() 
            => _level.Value.ToString();
        
        private string GetMaxLevel() 
            => MaxLevel.ToString();
        
        private float GetStatValue()
            => _value.Value;
        
        private float GetNextValue()
            => _nextValue.Value;
        
        private void OnUpgradeCommand(Unit _)
        {
            if (CanLevelUp())
            {
                _upgradeSystem.OnLevelUp += OnLevelUp;
                _upgradeSystem.LevelUp(Id);
            }
        }

        private void OnLevelUp(Upgrade upgrade)
        {
            _upgradeSystem.OnLevelUp -= OnLevelUp;
            _level.Value = upgrade.Level;
            _price.Value = upgrade.NextPrice;
            _value.Value = upgrade.GetStatValue(_level.Value);
            _nextValue.Value = upgrade.GetStatValue(_level.Value + 1);
            UpdateButtonStateCommand.Execute();
        }
    }
}
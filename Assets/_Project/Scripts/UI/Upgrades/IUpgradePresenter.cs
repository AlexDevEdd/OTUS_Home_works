using _Project.Scripts.Upgrades;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI.Upgrades
{
    public interface IUpgradePresenter
    {
        public IReadOnlyReactiveProperty<int> Price { get; }
        public IReadOnlyReactiveProperty<int> Level { get; }
        public IReadOnlyReactiveProperty<float> Value { get; }
        public IReadOnlyReactiveProperty<float> NextValue { get; }
        
        public StatType Id { get; }
        public string Name { get; }
        public int MaxLevel { get; }
        public Sprite Icon { get; }
        
        public ReactiveCommand UpgradeCommand { get; }
        public ReactiveCommand UpdateButtonStateCommand { get; }
        public CompositeDisposable CompositeDisposable { get;}
        
        public void Subscribe();
        public void UnSubscribe();
        public bool CanLevelUp();
        public bool IsMaxLevel();
        public string GetPrice();
        public string GetConvertedValueText();
        public string GetConvertedLevelText();
    }
}
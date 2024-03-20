using _Game.Scripts.Tools;
using _Project.Scripts.UI.Upgrades.Views;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using PrefabProvider = _Project.Scripts.ScriptableConfigs.PrefabProvider;

namespace _Project.Scripts.UI.Upgrades.Factories
{
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
}
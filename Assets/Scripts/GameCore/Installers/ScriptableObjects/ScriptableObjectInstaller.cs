using UnityEngine;
using Zenject;

namespace GameCore.Installers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ScriptableObjectInstaller", menuName = "Installers/ScriptableObjectInstaller")]
    public class ScriptableObjectInstaller : ScriptableObjectInstaller<ScriptableObjectInstaller>
    {
        [SerializeField] private GameResources _gameResources;
        [SerializeField] private PrefabProvider _prefabs;
        [SerializeField] private GameBalance _balance;
        
        public override void InstallBindings()
        {
            BindingPrefabProvider();
            BindingBalance();
            BindGameResources();

        }
        
        private void BindingBalance()
        {
            Container.BindInterfacesAndSelfTo<GameBalance>()
                .FromInstance(_balance)
                .AsSingle();
        }
        
        private void BindingPrefabProvider()
        {
            Container.BindInterfacesAndSelfTo<PrefabProvider>()
                .FromInstance(_prefabs)
                .AsSingle();
        }

        private void BindGameResources()
        {
            Container.BindInterfacesAndSelfTo<GameResources>()
                .FromInstance(_gameResources)
                .AsSingle();
        }
    }
}
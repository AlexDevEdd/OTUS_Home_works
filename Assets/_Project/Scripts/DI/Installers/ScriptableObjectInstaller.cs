using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using UnityEngine;
using Zenject;
using PrefabProvider = _Project.Scripts.EcsEngine._OOP.ScriptableConfigs.PrefabProvider;


namespace _Project.Scripts.DI.Installers
{
    [CreateAssetMenu(fileName = "ScriptableObjectInstaller", menuName = "Installers/ScriptableObjectInstaller")]
    public class ScriptableObjectInstaller : ScriptableObjectInstaller<ScriptableObjectInstaller>
    {
        // [SerializeField, Space] private GameResources _gameResources;
        [SerializeField, Space] private PrefabProvider _prefabs;
        [SerializeField, Space] private GameBalance _balance;
        
        public override void InstallBindings()
        {
            BindingPrefabProvider();
            BindingBalance();
            // BindGameResources();
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

        // private void BindGameResources()
        // {
        //     Container.BindInterfacesAndSelfTo<GameResources>()
        //         .FromInstance(_gameResources)
        //         .AsSingle();
        // }
    }
}
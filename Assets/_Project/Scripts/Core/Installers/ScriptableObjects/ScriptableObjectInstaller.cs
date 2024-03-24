using _Project.Scripts.CustomPool;
using _Project.Scripts.ScriptableConfigs;
using UnityEngine;
using Zenject;
using PrefabProvider = _Project.Scripts.ScriptableConfigs.PrefabProvider;

namespace _Project.Scripts.Core.Installers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ScriptableObjectInstaller", menuName = "Installers/ScriptableObjectInstaller")]
    public class ScriptableObjectInstaller : ScriptableObjectInstaller<ScriptableObjectInstaller>
    {
        [SerializeField, Space] private GameBalance _balance;
        [SerializeField, Space] private PrefabProvider _prefabs;
        [SerializeField, Space] private GameResources _resources;
        
        public override void InstallBindings()
        {
            SetUpContainerForPools();
            BindingPrefabProvider();
            BindingBalance();
            BindingGameResources();
        }

        private void SetUpContainerForPools()
        {
            //_prefabs.WorldSpaceContainer = FindObjectOfType<WorldSpaceContainer>().transform;
            _prefabs.PoolsContainer = FindObjectOfType<PoolsContainer>().transform;
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
        
        private void BindingGameResources()
        {
            Container.BindInterfacesAndSelfTo<GameResources>()
                .FromInstance(_resources)
                .AsSingle();
        }
    }
}
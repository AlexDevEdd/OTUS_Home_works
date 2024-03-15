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
        [SerializeField, Space] private PrefabProvider _prefabs;
        [SerializeField, Space] private GameBalance _balance;
        
        public override void InstallBindings()
        {
            SetUpContainerForPools();
            BindingPrefabProvider();
            BindingBalance();
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
    }
}
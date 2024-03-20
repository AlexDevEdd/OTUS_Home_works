using System;
using _Project.Scripts.ScriptableConfigs.Configs;
using Plugins.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "GameBalance", menuName = "Installers/GameBalance")]
    public class GameBalance : ScriptableObject
    {
        public int StartMoney = 10000;
        
        [Title("App step configurations", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField] 
        private SerializableDictionary<int, AppStepConfig> _appStepConfigs;

        public AppStepConfig GetAppStepConfig(int id)
        {
            if(_appStepConfigs.TryGetValue(id, out var config))
                return config;

            throw new ArgumentException($"Config with type of {id} doesn't exist");
        }
    }
}
using System;
using Plugins.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.ScriptableConfigs
{
    [Serializable]
    public class GameBalance
    {
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
    
    [Serializable]
    public struct AppStepConfig
    {
        public float Duration;
        public string TitleText;
    }
}
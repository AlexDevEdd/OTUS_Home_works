using System;
using Plugins.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.ScriptableConfigs
{
    [Serializable]
    public class PrefabProvider
    {
        public Transform PoolsContainer;
        public Transform WorldSpaceContainer;
        
        [Title("Prefab References", TitleAlignment = TitleAlignments.Centered)]
        [Space, SerializeField]
        private SerializableDictionary<string, GameObject> _prefabReferences;
        
        public T GetPrefab<T>(string key) where T : MonoBehaviour
        {
            if (_prefabReferences.TryGetValue(key, out var prefab))
                return prefab as T;
        
            throw new ArgumentException($"Doesn't exist KEY of {key}");
        }
    }
}
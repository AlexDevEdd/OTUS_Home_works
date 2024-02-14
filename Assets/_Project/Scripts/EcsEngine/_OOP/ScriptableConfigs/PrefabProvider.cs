using System;
using Leopotam.EcsLite.Entities;
using Plugins.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.ScriptableConfigs
{
    [Serializable]
    public class PrefabProvider
    {
        [Title("Prefab References", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField]
        private SerializableDictionary<string, Entity> _prefabReferences;
        
        public Entity GetPrefab(string key)
        {
            if (_prefabReferences.TryGetValue(key, out var prefab))
                return prefab;
        
            throw new ArgumentException($"Doesn't exist KEY of {key}");
        }
    }
}
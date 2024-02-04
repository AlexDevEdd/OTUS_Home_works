using System;
using _Project.Scripts.GameEngine.Interfaces;
using _Project.Scripts.Tools;
using _Project.Scripts.Tools.Serialize;
using Sirenix.OdinInspector;
using UnityEngine;


namespace _Project.Scripts.DI.Installers.ScriptableObjects
{
    [Serializable]
    public class PrefabProvider : IPrefabProvider
    {
        [Title("Prefab References", TitleAlignment = TitleAlignments.Centered)]
        [SerializeField]private SerializableDictionary<string, GameObject> _prefabReferences;
        
        public T GetPrefab<T>(string key) where T: MonoBehaviour
        {
            if (_prefabReferences.TryGetValue(key, out var prefab))
                return prefab.GetOrAddComponent<T>();
        
            throw new ArgumentException($"Doesn't exist KEY of {key}");
        }
    }
}

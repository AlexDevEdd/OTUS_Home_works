using System;
using Game.Scripts.Tools;
using Scripts2;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Installers.ScriptableObjects
{
    [Serializable]
    public class PrefabProvider
    {
        [Title("PrefabReferences", TitleAlignment = TitleAlignments.Centered)]
        public SerializableDictionary<string, GameObject> PrefabReferences;

        public T GetPrefab<T>(string key) where T: MonoBehaviour
        {
            if (PrefabReferences.TryGetValue(key, out var prefab))
                return prefab.GetOrAddComponent<T>();

            throw new ArgumentException($"Does not exist KEY of {key}");
        }
    }
}
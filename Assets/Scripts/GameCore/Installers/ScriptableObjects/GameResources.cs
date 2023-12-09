using System;
using Scripts2;
using UnityEngine;

namespace GameCore.Installers.ScriptableObjects
{
    [Serializable]
    public class GameResources
    {
        [SerializeField] private SerializableDictionary<string, Sprite> _icons;
        [SerializeField] private Sprite _placeholder;
        
        public Sprite GetSprite(string key)
        {
            return _icons.TryGetValue(key, out var sprite)
                ? sprite
                : _placeholder;
        }
        
        public Sprite GetSprite<T>(T type) where T: Enum
        { 
             return _icons.TryGetValue(type.ToString(), out var sprite)
                 ? sprite
                 : _placeholder;
        }
    }
}
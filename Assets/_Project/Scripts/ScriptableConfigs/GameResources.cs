using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "GameResources", menuName = "Installers/GameResources")]
    public class GameResources : ScriptableObject
    {
        [SerializeField] private List<Sprite> _icons;
        [SerializeField] private List<Sprite> _inventoryIcons;
        
        public Sprite GetInventoryItemIcon(string key)
        {
            var result = _inventoryIcons.FirstOrDefault(s =>
                string.Compare(s.name, key, StringComparison.OrdinalIgnoreCase) == 0);
            
            return result;
        }
        
        public Sprite GetSprite(string key)
        {
            var result = _icons.FirstOrDefault(s => s.name == key);
            
            return result == null 
                ? throw new ArgumentException($"Doesn't exist icon with key {key}") 
                : result;
        }
        
        public Sprite GetSprite<T>(T type) where T: Enum
        {
            var result = _icons.FirstOrDefault(s => s.name.Equals(type.ToString()));
            
            return result == null 
                ? throw new ArgumentException($"Doesn't exist icon with key {type}") 
                : result;
        }
    }
}
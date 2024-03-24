using System;
using _Project.Scripts.Inventory.Data;
using _Project.Scripts.ScriptableConfigs.Configs;
using Plugins.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "GameBalance", menuName = "Installers/GameBalance")]
    public class GameBalance : ScriptableObject
    {
        [Title("App step configurations", TitleAlignment = TitleAlignments.Centered)] [PropertySpace]
        [Space(10)]
        public AppStepConfiguration AppStepConfigs;

        [Title("Inventory configurations", TitleAlignment = TitleAlignments.Centered)]
        [Space(10)]
        public InventoryConfiguration InventoryConfigs;
    }

    [Serializable]
    public sealed class AppStepConfiguration
    {
        [SerializeField] 
        private SerializableDictionary<int, AppStepConfig> _configs;
        
        public AppStepConfig GetConfig(int id)
        {
            if(_configs.TryGetValue(id, out var config))
                return config;

            throw new ArgumentException($"Config with type of {id} doesn't exist");
        }
    }
    
    [Serializable]
    public sealed class InventoryConfiguration
    {
        [SerializeField] [Header("Item list")] [Space(5)]
        private SerializableDictionary<string, ItemData> _items;
        
        [SerializeField] [Header("Start inventory configs")] [Space(5)]
        private SerializableDictionary<string, InventoryData> _startConfigs;

        public InventoryData GetStartConfig(string inventoryId)
        {
            if(_startConfigs.TryGetValue(inventoryId, out var config))
                return config;
        
            throw new ArgumentException($"Config with type of {inventoryId} doesn't exist");
        }
        
        public ItemData GetItemData(string itemId)
        {
            if(_items.TryGetValue(itemId, out var config))
                return config;
        
            throw new ArgumentException($"Data with ID {itemId} doesn't exist");
        }
    }
}
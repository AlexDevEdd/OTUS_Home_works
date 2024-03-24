using System;
using System.Collections.Generic;
using _Project.Scripts.Inventory.Data;
using _Project.Scripts.Inventory.Interfaces;
using _Project.Scripts.Inventory.Structures;
using _Project.Scripts.ScriptableConfigs;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    [UsedImplicitly]
    public sealed class InventorySystem
    {
        private readonly GameResources _resources;
        private readonly Dictionary<string, IInventoryPresenter> _inventoryMap = new ();
        private readonly GameBalance _balance;
        
        public InventorySystem(GameResources resources, GameBalance balance)
        {
            _resources = resources;
            _balance = balance;
        }

        public InventoryPresenter RegisterInventory(InventoryData inventoryData)
        {
            var inventory = new InventoryPresenter(inventoryData, _resources, _balance);
            _inventoryMap[inventory.OwnerId] = inventory;
            return inventory;
        }
        
        public InventoryPresenter RegisterInventory(string id)
        {
            var data = _balance.InventoryConfigs.GetStartConfig(id);
            var inventory = new InventoryPresenter(data, _resources, _balance);
            _inventoryMap[inventory.OwnerId] = inventory;
            return inventory;
        }

        public AddItemsToInventoryResult AddItemsToInventory(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoryMap[ownerId];
            var data = _balance.InventoryConfigs.GetItemData(itemId);
            return inventory.AddItems(itemId, amount);
        }
        
        public AddItemsToInventoryResult AddItemsToInventory(string ownerId, Vector2Int slotCoords, string itemId, int amount = 1)
        {
            var inventory = _inventoryMap[ownerId];
            return inventory.AddItems(slotCoords, itemId, amount);
        }

        
        public RemoveItemsFromInventoryResult RemoveItems(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoryMap[ownerId];
            return inventory.RemoveItems(itemId, amount);
        }
        
        public RemoveItemsFromInventoryResult RemoveItems(Vector2Int slotCoords, string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoryMap[ownerId];
            return inventory.RemoveItems(slotCoords, itemId, amount);
        }
        
        public bool Has(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoryMap[ownerId];
            return inventory.Has(itemId, amount);
        }
        
        public int GetAmount(string ownerId, string itemId)
        {
            var inventory = _inventoryMap[ownerId];
            return inventory.GetAmount(itemId);
        }

        public IInventoryPresenter GetInventory(string ownerId)
        {
            return _inventoryMap[ownerId];
        }
    }
    
    [Flags]
    public enum ItemFlags
    {
        None = 0,
        Stackable = 1,
        Consumable = 2,
        Equippable = 4,
        Effectable = 8
    }
}
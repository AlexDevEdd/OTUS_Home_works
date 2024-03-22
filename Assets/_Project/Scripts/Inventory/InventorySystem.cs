using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public sealed class InventorySystem
    {
        private readonly Dictionary<string, InventoryGrid> _inventoryMap = new ();

        public InventoryGrid RegisterInventory(InventoryGridData inventoryGridData)
        {
            var inventory = new InventoryGrid(inventoryGridData);
            _inventoryMap[inventory.OwnerId] = inventory;
            return inventory;
        }

        public AddItemsToInventoryResult AddItemsToInventory(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoryMap[ownerId];
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

        public IInventoryGrid GetInventory(string ownerId)
        {
            return _inventoryMap[ownerId];
        }
    }
}
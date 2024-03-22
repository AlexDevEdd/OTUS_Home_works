using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventoryGrid : IInventoryGrid
    {
        public event Action<string, int> OnItemsAdded;
        public event Action<string, int> OnItemsRemoved;
        public event Action<Vector2Int> OnSizeChanged;

        public string OwnerId => _data.OwnerId;
        public Vector2Int Size
        {
            get => _data.Size;
            set
            {
                if (_data.Size != value)
                {
                    _data.Size = value;
                    OnSizeChanged?.Invoke(value);
                }
            }
        }

        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();
        public InventoryGrid(InventoryGridData data)
        {
            _data = data;

            var size = data.Size;
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var index = i * size.y + j;
                    var slotData = data.Slots[index];
                    var slot = new InventorySlot(slotData);
                    var position = new Vector2Int(i, j);
                    _slotsMap[position] = slot;
                }
            }
        }

        public AddItemsToInventoryResult AddItems(string itemId, int amount = 1)
        {
            var remainingAmount = amount;
            var itemsAddedToSlotWithSameItemsAmount = AddToSlotWithSameItems(itemId, remainingAmount, out remainingAmount);

            if (remainingAmount <= 0)
            {
                return new AddItemsToInventoryResult(OwnerId, amount, itemsAddedToSlotWithSameItemsAmount);
            }
            
            var itemsAddedToAvailableSlotAmount = AddToFirstAvailableSlots(itemId, remainingAmount, out remainingAmount);
            var totalAddedItemsAmount = itemsAddedToSlotWithSameItemsAmount + itemsAddedToAvailableSlotAmount;

            return new AddItemsToInventoryResult(OwnerId, amount, totalAddedItemsAmount);
        }
        
        public AddItemsToInventoryResult AddItems(Vector2Int slotCoords, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotCoords];
            var newValue = slot.Amount + amount;
            var itemsAddedAmount = 0;
            if (slot.IsEmpty)
            {
                slot.ItemId = itemId;
            }

            var itemSlotCapacity = GetItemSlotCapacity(itemId);

            if (newValue > itemSlotCapacity)
            {
                var remainingItems = newValue = itemSlotCapacity;
                var itemsToAddedAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddedAmount;

                var result = AddItems(itemId, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newValue;
            }

            return new AddItemsToInventoryResult(OwnerId, amount, itemsAddedAmount);
        }

        
        public RemoveItemsFromInventoryResult RemoveItems(string itemId, int amount = 1)
        {
            if (!Has(itemId, amount))
            {
                return new RemoveItemsFromInventoryResult(OwnerId, amount, false); 
            }

            var amountToRemove = amount;
            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var slotCoords = new Vector2Int(i, j);
                    var slot = _slotsMap[slotCoords];

                    if (slot.ItemId != itemId)
                    {
                        continue;
                    }
                    
                    if (amountToRemove > slot.Amount) 
                    {
                        amountToRemove -= slot.Amount;
                        RemoveItems(slotCoords, itemId, slot.Amount);
                    }
                    else
                    {
                        RemoveItems(slotCoords, itemId, amountToRemove); 
                        return new RemoveItemsFromInventoryResult(OwnerId, amount, true);
                    }
                }
            }

            throw new Exception("message");
        }
        
        public RemoveItemsFromInventoryResult RemoveItems(Vector2Int slotCoords, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotCoords];
            if (slot.IsEmpty || slot.ItemId != itemId || slot.Amount < amount)
            {
                return new RemoveItemsFromInventoryResult(OwnerId, amount, false);
            }

            slot.Amount -= amount;
            if (slot.Amount == 0)
            {
                slot.ItemId = null;
            }

            return new RemoveItemsFromInventoryResult(OwnerId, amount, true);
        }
        
        

        public int GetAmount(string itemId)
        {
            var slots = _data.Slots;

            return slots.Where(slot => slot.ItemId == itemId).Sum(slot => slot.Amount);
        }

        public bool Has(string itemId, int amount)
        {
            var amountExist = GetAmount(itemId);
            return amountExist >= amount;
        }

        public void SwitchSlots(Vector2Int slotCoorsA, Vector2Int slotCoorsB)
        {
            var slotA = _slotsMap[slotCoorsA];
            var slotB = _slotsMap[slotCoorsB];
            var tempSlotItemId = slotA.ItemId;
            var tempSlotItemAmount = slotA.Amount;

            slotA.ItemId = slotB.ItemId;
            slotA.Amount = slotB.Amount;
            slotB.ItemId = tempSlotItemId;
            slotB.Amount = tempSlotItemAmount;
        }

        public void SetSize(Vector2Int newSize)
        {
            //TODO: enum with size? like small is 3*4
        }

        public IInventorySlot[,] GetSlots()
        {
            var array = new IInventorySlot[Size.x, Size.y];
            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[position];
                }
            }

            return array;
        }
        
        private int GetItemSlotCapacity(string itemId)
        {
            return 99;
        }
        
        private int AddToSlotWithSameItems(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (slot.IsEmpty)
                    {
                        continue;
                    }
                    
                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId);
                    if (slot.Amount >= slotItemCapacity || slot.ItemId != itemId)
                    {
                        continue;
                    }
                    
                    var newValue = slot.Amount + remainingAmount;

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;
                        return itemsAddedAmount;
                    }

                }
            }
            
            return itemsAddedAmount;
        }
        
        private int AddToFirstAvailableSlots(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (slot.IsEmpty)
                    {
                        continue;
                    }

                    slot.ItemId = itemId;
                    var newValue = remainingAmount;
                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId);
                    
                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        itemsAddedAmount += slotItemCapacity;
                        slot.Amount = slotItemCapacity;

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;
                        return itemsAddedAmount;
                    }

                }
            }
            
            return itemsAddedAmount;
        }
    }
}
using System;
using System.Linq;
using _Project.Scripts.Inventory.Data;
using _Project.Scripts.Inventory.Interfaces;
using _Project.Scripts.Inventory.Structures;
using _Project.Scripts.ScriptableConfigs;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventoryPresenter : IInventoryPresenter
    {
        private readonly GameBalance _balance;
        private readonly InventoryData _data;
        private readonly ReactiveProperty<Vector2Int> _size;
        private readonly ReactiveDictionary<Vector2Int, IInventorySlotPresenter> _slotsMap = new();
        public IReadOnlyReactiveProperty<Vector2Int> Size => _size;
        public string OwnerId => _data.OwnerId;
        
        public InventoryPresenter(InventoryData data, GameResources resources, GameBalance gameBalance)
        {
            _data = data;
            _balance = gameBalance;
            _size = new ReactiveProperty<Vector2Int>(data.Size);
           
            for (var i = 0; i < _size.Value.x; i++)
            {
                for (var j = 0; j < _size.Value.y; j++)
                {
                    var index = i * _size.Value.y + j;
                    var slotData = data.Slots[index];
                    var position = new Vector2Int(i, j);
                    var slot = new InventorySlotPresenter(slotData, resources, position);
                    slot.EndDragCommand.Subscribe(OnCommand);
                    _slotsMap[position] = slot;
                }
            }
        }
        
        private void OnCommand(SwitchItemSlots coords)
        {
           SwitchSlots(coords.First, coords.Second);
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
            var newValue = slot.Amount.Value + amount;
            var itemsAddedAmount = 0;
            if (slot.IsEmpty.Value)
            {
                slot.SetItemId(itemId);
            }

            var itemSlotCapacity = GetItemSlotCapacity(itemId);

            if (newValue > itemSlotCapacity)
            {
                var remainingItems = newValue - itemSlotCapacity;
                var itemsToAddedAmount = itemSlotCapacity - slot.Amount.Value;
                itemsAddedAmount += itemsToAddedAmount;

                var result = AddItems(itemId, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
                slot.SetItemAmount(itemsAddedAmount);
            }
            else
            {
                itemsAddedAmount = amount;
                slot.SetItemAmount(newValue);
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
            for (var i = 0; i < Size.Value.x; i++)
            {
                for (var j = 0; j < Size.Value.y; j++)
                {
                    var slotCoords = new Vector2Int(i, j);
                    var slot = _slotsMap[slotCoords];

                    if (slot.ItemId.Value != itemId)
                    {
                        continue;
                    }
                    
                    if (amountToRemove > slot.Amount.Value) 
                    {
                        amountToRemove -= slot.Amount.Value;
                        RemoveItems(slotCoords, itemId, slot.Amount.Value);
                    }
                    else
                    {
                        RemoveItems(slotCoords, itemId, amountToRemove);
                        return new RemoveItemsFromInventoryResult(OwnerId, amount, true);
                    }
                }
            }

            throw new Exception($"Size for {OwnerId} is empty");
        }
        
        public RemoveItemsFromInventoryResult RemoveItems(Vector2Int slotCoords, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotCoords];
            if (slot.IsEmpty.Value || slot.ItemId.Value != itemId || slot.Amount.Value < amount)
            {
                return new RemoveItemsFromInventoryResult(OwnerId, amount, false);
            }

            var newAmount = slot.Amount.Value - amount;
            if (newAmount == 0)
            {
                slot.SetItemAmount(newAmount);
                slot.SetItemId(null);
                slot.SetIsEmpty(true);
                return new RemoveItemsFromInventoryResult(OwnerId, amount, true);
            }
            
            slot.SetItemAmount(newAmount);
            return new RemoveItemsFromInventoryResult(OwnerId, amount, true);
        }
        
        public int GetAmount(string itemId)
        {
            return  _slotsMap.Values.Where(slot => slot.ItemId.Value == itemId).Sum(slot => slot.Amount.Value);
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
            var tempSlotItemId = slotA.ItemId.Value;
            var tempSlotItemAmount = slotA.Amount.Value;
            var tempSlotCapacity = slotA.SlotCapacity;
            
            slotA.SetItemId(slotB.ItemId.Value);
            slotA.SetItemAmount(slotB.Amount.Value);
            slotA.SetSlotCapacity(slotB.SlotCapacity);
            
            if (slotB.IsEmpty.Value) 
                slotA.SetIsEmpty(true);

            slotB.SetItemId(tempSlotItemId);
            slotB.SetItemAmount(tempSlotItemAmount);
            slotB.SetSlotCapacity(tempSlotCapacity);
            
            if (slotB.IsEmpty.Value) 
                slotB.SetIsEmpty(false);
        }

        public void SetSize(Vector2Int newSize)
        {
            //TODO: enum with size? like small is 3*4
        }

        public IInventorySlotPresenter[,] GetSlots()
        {
            var array = new IInventorySlotPresenter[Size.Value.x, Size.Value.y];
            for (int i = 0; i < Size.Value.x; i++)
            {
                for (int j = 0; j < Size.Value.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[position];
                }
            }

            return array;
        }
        
        private int GetItemSlotCapacity(string itemId)
        {
            return _balance.InventoryConfigs.GetItemData(itemId).SlotCapacity;
        }
        
        private int AddToSlotWithSameItems(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.Value.x; i++)
            {
                for (int j = 0; j < Size.Value.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (slot.IsEmpty.Value)
                    {
                        continue;
                    }
                    
                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId.Value);
                    if (slot.Amount.Value >= slotItemCapacity || slot.ItemId.Value != itemId)
                    {
                        continue;
                    }
                    
                    var newValue = slot.Amount.Value + remainingAmount;

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount.Value;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.SetItemAmount(slotItemCapacity);

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.SetItemAmount(newValue);
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

            for (int i = 0; i < Size.Value.x; i++)
            {
                for (int j = 0; j < Size.Value.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (!slot.IsEmpty.Value)
                    {
                        continue;
                    }

                    slot.SetItemId(itemId);
                    slot.SetIsEmpty(false);
                    var newValue = remainingAmount;
                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId.Value);
                    
                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        itemsAddedAmount += slotItemCapacity;
                        slot.SetItemAmount(slotItemCapacity);

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.SetItemAmount(newValue);
                        remainingAmount = 0;
                        return itemsAddedAmount;
                    }

                }
            }
            
            return itemsAddedAmount;
        }
    }
}
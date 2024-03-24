using System;
using _Project.Scripts.Inventory.Data;
using _Project.Scripts.Inventory.Interfaces;
using _Project.Scripts.ScriptableConfigs;
using _Project.Scripts.Tools;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventorySlotPresenter : IInventorySlotPresenter
    {
        private readonly GameResources _resources;
        public event Action<string, Vector2> OnBeginDrag;
        public event Action<Vector2> OnDrag;
        public event Action<string, Vector2Int> OnEndDrag;
        
        private readonly ReactiveProperty<string> _itemId;
        private readonly ReactiveProperty<int> _amount;
        private readonly ReactiveProperty<bool> _isEmpty;
        
        public IReadOnlyReactiveProperty<string> ItemId => _itemId;
        public IReadOnlyReactiveProperty<int> Amount => _amount;
        public IReadOnlyReactiveProperty<bool> IsEmpty => _isEmpty;
        public ReactiveCommand<SwitchItemSlots> BeginDragCommand { get; }
        public ReactiveCommand<SwitchItemSlots> DragCommand { get; }
        public ReactiveCommand<SwitchItemSlots> EndDragCommand { get; }
        public Sprite Icon { get; private set; }
        public int SlotCapacity { get; private set; }
        public Vector2Int Position { get; }
        public InventorySlotPresenter(InventorySlotData data, GameResources resources, Vector2Int vector2Int)
        {
            _resources = resources;
            Position = vector2Int;
            _itemId = new ReactiveProperty<string>(data.ItemId);
            _amount = new ReactiveProperty<int>(data.Amount);
            _isEmpty = new ReactiveProperty<bool>(_amount.Value == 0 && string.IsNullOrEmpty(_itemId.Value));
            SlotCapacity = data.SlotCapacity;
            BeginDragCommand = new ReactiveCommand<SwitchItemSlots>();
            DragCommand = new ReactiveCommand<SwitchItemSlots>();
            EndDragCommand = new ReactiveCommand<SwitchItemSlots>();
           
            if(!data.ItemId.IsNullOrEmpty())
                Icon = _resources.GetInventoryItemIcon(_itemId.Value);
        }
        
        public void SetItemId(string itemId)
        {
            var icon = _resources.GetInventoryItemIcon(itemId);
            Icon = icon;
            _itemId.Value = itemId;
        }
        
        public void SetItemAmount(int amount)
        {
            _amount.Value = amount;
        }
        
        public void SetSlotCapacity(int maxSlotCount)
        {
            SlotCapacity = maxSlotCount;
        }
        
        public void SetIsEmpty(bool isEmpty)
        {
            _isEmpty.Value = isEmpty;
        }

        public string GetConvertedItemAmount()
        {
            return _amount.Value.ToString();
        }
    }
}
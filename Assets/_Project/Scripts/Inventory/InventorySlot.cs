using System;

namespace _Project.Scripts.Inventory
{
    public class InventorySlot : IInventorySlot
    {
        public event Action<string> OnItemIdChanged;
        public event Action<int> OnItemAmountChanged;
        
        public string ItemId
        {
            get => _data.ItemId;
            set
            {
                if (_data.ItemId != value)
                {
                    _data.ItemId = value;
                    OnItemIdChanged?.Invoke(value);
                }
            }
        }

        public int Amount
        {
            get => _data.Amount;
            set
            {
                if (_data.Amount != value)
                {
                    _data.Amount = value;
                    OnItemAmountChanged?.Invoke(value);
                }
            }
        }

        public bool IsEmpty => Amount == 0 && string.IsNullOrEmpty(ItemId);

        private readonly InventorySlotData _data;

        public InventorySlot(InventorySlotData data)
        {
            _data = data;
        }
    }
}
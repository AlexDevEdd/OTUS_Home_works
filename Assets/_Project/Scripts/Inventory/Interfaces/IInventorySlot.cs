using System;

namespace _Project.Scripts.Inventory
{
    public interface IInventorySlot
    {
        public event Action<string> OnItemIdChanged; 
        public event Action<int> OnItemAmountChanged;
        
        public string ItemId { get; }
        public int Amount { get; }
        public bool IsEmpty { get; }
    }
}
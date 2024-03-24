using System;

namespace _Project.Scripts.Inventory.Data
{
    [Serializable]
    public class InventorySlotData
    {
        public string ItemId;
        public int Amount;
        public int SlotCapacity = 99;
    }
}
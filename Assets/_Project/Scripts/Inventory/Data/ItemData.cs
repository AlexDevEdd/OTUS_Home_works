using System;

namespace _Project.Scripts.Inventory.Data
{
    [Serializable]
    public class ItemData
    {
        public string ItemId;
        public int SlotCapacity = 99;
        public ItemFlags Mode;
    }
}
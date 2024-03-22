using System;
using UniRx;

namespace _Project.Scripts.Inventory
{
    [Serializable]
    public class InventorySlotData
    {
        public string ItemId;
        public int Amount;
    }
}
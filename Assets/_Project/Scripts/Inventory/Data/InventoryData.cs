using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Inventory.Data
{
    [Serializable]
    public class InventoryData
    {
        public string OwnerId;
        public List<InventorySlotData> Slots;
        public Vector2Int Size;
    }
}
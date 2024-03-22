using System;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public interface IInventoryGrid : IInventory
    {
        public event Action<Vector2Int> OnSizeChanged;
        Vector2Int Size { get; }
        IInventorySlot[,] GetSlots();
    }
}
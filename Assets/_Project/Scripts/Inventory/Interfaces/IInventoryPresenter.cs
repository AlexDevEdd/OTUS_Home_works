using _Project.Scripts.Inventory.Structures;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Inventory.Interfaces
{
    public interface IInventoryPresenter
    {
        public string OwnerId { get; }
        public IReadOnlyReactiveProperty<Vector2Int> Size { get; }
        public int GetAmount(string itemId);
        public bool Has(string itemId, int amount);
        IInventorySlotPresenter[,] GetSlots();
        public AddItemsToInventoryResult AddItems(string itemId, int amount = 1);
        public AddItemsToInventoryResult AddItems(Vector2Int slotCoords, string itemId, int amount = 1);
        public RemoveItemsFromInventoryResult RemoveItems(string itemId, int amount = 1);
        public RemoveItemsFromInventoryResult RemoveItems(Vector2Int slotCoords, string itemId, int amount = 1);
    }
}
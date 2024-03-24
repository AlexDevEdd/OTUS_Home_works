using UniRx;
using UnityEngine;

namespace _Project.Scripts.Inventory.Interfaces
{
    public interface IInventorySlotPresenter
    {
        public IReadOnlyReactiveProperty<string> ItemId { get; }
        public IReadOnlyReactiveProperty<int> Amount { get; }
        public IReadOnlyReactiveProperty<bool> IsEmpty { get; }
        public int SlotCapacity { get; }
        public Sprite Icon { get; }
        public Vector2Int Position { get; }
        public void SetItemId(string itemId);
        public void SetItemAmount(int amount);
        public void SetSlotCapacity(int maxSlotCount);
        public void SetIsEmpty(bool isEmpty);
        public string GetConvertedItemAmount();
        public ReactiveCommand<SwitchItemSlots> BeginDragCommand { get; }
        public ReactiveCommand<SwitchItemSlots> DragCommand { get; }
        public ReactiveCommand<SwitchItemSlots> EndDragCommand { get; }
    }
}
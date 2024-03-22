namespace _Project.Scripts.Inventory.Controllers
{
    public sealed class InventorySlotController
    {
        private readonly InventorySlotView _view;

        public InventorySlotController(IInventorySlot slot, InventorySlotView view)
        {
            _view = view;
            
            slot.OnItemIdChanged += OnItemIdChanged;
            slot.OnItemAmountChanged += OnItemAmountChanged;
        }

        private void OnItemAmountChanged(int newAmount)
        {
            _view.SetAmount(newAmount.ToString());
        }

        private void OnItemIdChanged(string newItemId)
        {
            _view.SetTitle(newItemId);
        }
    }
}
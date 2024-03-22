namespace _Project.Scripts.Inventory.Controllers
{
    public sealed class ScreenController
    {
        private readonly InventorySystem _inventorySystem;
        private readonly ScreenView _view;

        private InventoryGridController _currentInventoryGridController;
        
        public ScreenController(InventorySystem inventorySystem, ScreenView view)
        {
            _inventorySystem = inventorySystem;
            _view = view;
        }

        public void OpenInventory(string ownerId)
        {
            var inventory = _inventorySystem.GetInventory(ownerId);
            var inventoryView = _view.InventoryView.GridView;

            _currentInventoryGridController = new InventoryGridController(inventory, inventoryView);
        }
    }
}
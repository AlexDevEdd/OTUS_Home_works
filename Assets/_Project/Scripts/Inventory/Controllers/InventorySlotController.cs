using _Project.Scripts.Inventory.Interfaces;
using _Project.Scripts.Inventory.Views;
using _Project.Scripts.ScriptableConfigs;

namespace _Project.Scripts.Inventory.Controllers
{
    public sealed class InventorySlotController
    {
        private readonly InventorySlot _view;
        private readonly GameResources _resources;

        public InventorySlotController(IInventorySlotPresenter slotPresenter, InventorySlot view, GameResources resources)
        {
            _view = view;
            _resources = resources;

           // slotPresenter.OnItemIdChanged += OnItemIdChanged;
           // slotPresenter.OnItemAmountChanged += OnItemAmountChanged;
        }

        private void OnItemAmountChanged(int newAmount)
        {
            //_view.SetAmount(newAmount.ToString());
        }

        private void OnItemIdChanged(string newItemId)
        {
            var sprite = _resources.GetInventoryItemIcon(newItemId);
           // _view.SetSprite(sprite);
        }
    }
}
using System.Collections.Generic;

namespace _Project.Scripts.Inventory.Controllers
{
    public sealed class InventoryGridController
    {
        private readonly List<InventorySlotController> _slotControllers = new();

        public InventoryGridController(IInventoryGrid inventory , InventoryGridView view)
        {
            var size = inventory.Size;
            var slots = inventory.GetSlots();
            var lineLenght = size.y;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var index = i * lineLenght + j;
                    var slotView = view.GetInventorySlotView(index);
                    var slot = slots[i, j];
                    _slotControllers.Add(new InventorySlotController(slot, slotView));
                }
            }
        }
    }
}
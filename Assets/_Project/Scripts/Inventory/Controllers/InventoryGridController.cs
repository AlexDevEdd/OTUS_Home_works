using System.Collections.Generic;
using _Project.Scripts.Inventory.Interfaces;
using _Project.Scripts.Inventory.Views;

namespace _Project.Scripts.Inventory.Controllers
{
    public sealed class InventoryGridController
    {
        private readonly List<InventorySlotController> _slotControllers = new();

        // public InventoryGridController(IInventoryGridPresenter inventoryGridPresenter , InventoryGridView view)
        // {
        //     var size = inventoryGridPresenter.Size;
        //     var slots = inventoryGridPresenter.GetSlots();
        //     var lineLenght = size.y;
        //
        //     for (int i = 0; i < size.x; i++)
        //     {
        //         for (int j = 0; j < size.y; j++)
        //         {
        //             var index = i * lineLenght + j;
        //             var slotView = view.GetInventorySlotView(index);
        //             var slot = slots[i, j];
        //            // _slotControllers.Add(new InventorySlotController(slot, slotView));
        //         }
        //     }
        // }
    }
}
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventoryGridView : MonoBehaviour
    {
        [SerializeField] private InventorySlotView[] _slots;
        
        public void SetUp(IInventoryGrid grid)
        {
            
        }
        
        public InventorySlotView GetInventorySlotView(int index)
        {
            return _slots[index];
        }
    }
}
using _Game.Scripts.Tools;
using _Project.Scripts.Inventory.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Inventory.Views
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private InventorySlot[] _slots;

        [Inject] private InventorySystem _inventorySystem;
        private const string KEY = "player";
        
        [Button]
        public void SetInventory()
        {
            SetUp(_inventorySystem.RegisterInventory(KEY));
        }
        
        [Button]
        public void AddApple(int value)
        {
           var result = _inventorySystem.AddItemsToInventory(KEY, "apple", value);
           if(result.ItemsNotAddedAmount != 0)
            Log.ColorLog($"Not added {result.ItemsNotAddedAmount}", ColorType.Orange);
        }
        
        [Button]
        public void AddMeat(int value)
        {
            _inventorySystem.AddItemsToInventory(KEY, "meat", value);
        }
        
        [Button]
        public void AddAxe(int value)
        {
            _inventorySystem.AddItemsToInventory(KEY, "axe", value);
        }
        
        [Button]
        public void AddShield(int value)
        {
            _inventorySystem.AddItemsToInventory(KEY, "shield", value);
        }
        
        [Button]
        public void AddHP(int value)
        {
            _inventorySystem.AddItemsToInventory(KEY, "hp", value);
        }
        
        [Button]
        public void GetAmount(string value)
        {
           var result = _inventorySystem.GetAmount(KEY, value);
           Log.ColorLog($"Result for {value} is:  {result}");
        }
        
        [Button]
        public void Remove(string valueId, int amount)
        {
           var result = _inventorySystem.RemoveItems(KEY,valueId, amount);
            Log.ColorLog($"Removed is {result.IsSuccess} with:  {result.ItemsToRemoveAmount}", ColorType.Red);
        }
        
        private IInventoryPresenter _inventoryPresenter;
        public void SetUp(IInventoryPresenter inventoryPresenter)
        {
            _inventoryPresenter = inventoryPresenter;
            
            var size = inventoryPresenter.Size;
            var slots = inventoryPresenter.GetSlots();
            var lineLenght = size.Value.y;
            
            for (int i = 0; i < size.Value.x; i++)
            {
                for (int j = 0; j < size.Value.y; j++)
                {
                    var index = i * lineLenght + j;
                    var slotView = _slots[index];
                    var slot = slots[i, j];
                    slotView.SetUp(slot);
                }
            }
        }
    }
    
    public interface IWindow
    {
        public void Open(object arg);
        public void Close();
    }
}
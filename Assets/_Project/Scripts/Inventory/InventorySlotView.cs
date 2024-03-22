using TMPro;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _amountText;
        
        public void SetTitle(string title)
        {
            _titleText.text = title;
        }
        
        public void SetAmount(string amount)
        {
            _amountText.text = amount;
        }
    }
}
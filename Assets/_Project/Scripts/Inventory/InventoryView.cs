using TMPro;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventoryGridView _gridView;
        [SerializeField] private TMP_Text _ownerId;

        public string OwnerId
        {
            get => _ownerId.text;
            set => _ownerId.text = value;
        }

        public InventoryGridView GridView => _gridView;
    }
}
using System;
using System.Collections.Generic;
using Sample;

namespace Equipment
{
    //TODO: Реализовать экипировку
    public sealed class Equipment
    {
        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;
        public event Action<Item> OnItemChanged; 

        public void Setup(KeyValuePair<EquipmentType, Item> items)
        {
            throw new NotImplementedException();
        }

        public Item GetItem(EquipmentType type)
        {
            throw new NotImplementedException();
        }

        public bool TryGetItem(EquipmentType type, out Item result)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(EquipmentType type, Item item)
        {
            throw new NotImplementedException();
        }

        public void AddItem(EquipmentType type, Item item)
        {
            throw new NotImplementedException();
        }

        public void ChangeItem(EquipmentType type, Item item)
        {
            throw new NotImplementedException();
        }

        public bool HasItem(EquipmentType type)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<EquipmentType, Item>[] GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
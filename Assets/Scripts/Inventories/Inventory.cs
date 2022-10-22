using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventories
{
    public abstract class Inventory : MonoBehaviour
    {
        public event Action<List<InventoryItem>> OnInventoryChanged;

    
        protected List<InventoryItem> _items;

        public void Awake()
        {
            _items = new List<InventoryItem>();
        }

        /// <summary>
        /// Add Item to StoreHouse
        /// </summary>
        /// <param name="inventoryItem">information about plant and amount</param>
        public virtual void AddItem(InventoryItem inventoryItem)
        {
            foreach (var item in _items)
            {
                if (item.plantInformation.Equals(inventoryItem.plantInformation))
                {
                    item.amount += inventoryItem.amount;
                    CallInventoryChanged();
                    return;
                }
            }
        
            _items.Add(inventoryItem);

            CallInventoryChanged();
        }

        protected void CallInventoryChanged()
        {
            OnInventoryChanged?.Invoke(_items);
        }
        
        
        public List<InventoryItem> GetItems()
        {
            return _items;
        }
        
    }
}

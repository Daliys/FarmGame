using System;
using UnityEngine;

namespace Inventories
{
    public class PlayerInventory : Inventory
    {
        [SerializeField] private int inventoryCapacity;


        public override void AddItem(InventoryItem inventoryItem)
        {
            if(IsInventoryFull()) return;
            
            _items.Add(inventoryItem);

            CallInventoryChanged();
        }


        /// <summary>
        /// Check is the inventory Full
        /// </summary>
        /// <returns>is the inventory Full</returns>
        public bool IsInventoryFull()
        {
            return _items.Count >= inventoryCapacity;
        }
        public int InventoryCapacity => inventoryCapacity;
    }
}
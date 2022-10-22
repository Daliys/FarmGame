using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIS
{
    public class UIPlayerInventory : MonoBehaviour
    {
        [SerializeField] private UIInventoryIndicator[] indicators;
        

        private void OnInventoryChanged(List<InventoryItem> inventoryItems)
        {        print("Changed");
            for (int i = 0, j = inventoryItems.Count; i < indicators.Length; i++)
            {
                if(i < j) indicators[i].ChangeToFull();
                else indicators[i].ChangeToEmpty();
            }
        }

        private void Start()
        {
            REF.Instance.PlayerInventory.OnInventoryChanged += OnInventoryChanged;
        }

        private void OnDisable()
        {
            REF.Instance.PlayerInventory.OnInventoryChanged -= OnInventoryChanged;
        }
    }
}
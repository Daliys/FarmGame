using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject storeHouseLocation;
    
    
    public static Inventory Instance;
    
    public static event Action<List<InventoryItem>> OnInventoryChaned;

    
    private List<InventoryItem> _items;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        _items = new List<InventoryItem>();
    }

    /// <summary>
    /// Add Item to StoreHouse
    /// </summary>
    /// <param name="inventoryItem">information about plant and amount</param>
    public void AddItem(InventoryItem inventoryItem)
    {
        foreach (var item in _items)
        {
            if (item.plantInformation.Equals(inventoryItem.plantInformation))
            {
                item.amount += inventoryItem.amount;
                OnInventoryChaned?.Invoke(_items);
                return;
            }
        }
        
        _items.Add(inventoryItem);
        
        OnInventoryChaned?.Invoke(_items);
    }


    public List<InventoryItem> GetItems()
    {
        return _items;
    }
    

    /// <summary>
    /// Get Vector3 Position of the Store House location 
    /// </summary>
    public Vector3 GetStoreHouseLocation()
    {
        return storeHouseLocation.transform.position;
    }
}

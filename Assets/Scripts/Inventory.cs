using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject storeHouseLocation;
    
    private List<InventoryItem> _items;
    
    public void Awake()
    {
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
                return;
            }
        }
        
        _items.Add(inventoryItem);
        
        print(inventoryItem.plantInformation + " " + inventoryItem.amount);
    }
    
    
    

    /// <summary>
    /// Get Vector3 Position of the Store House location 
    /// </summary>
    public Vector3 GetStoreHouseLocation()
    {
        return storeHouseLocation.transform.position;
    }
}

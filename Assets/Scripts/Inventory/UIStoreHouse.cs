using System;
using System.Collections.Generic;
using UnityEngine;

public class UIStoreHouse : MonoBehaviour
{
    [SerializeField] private GameObject uiItemPrefab;
    [SerializeField] private GameObject parentToAddPrefab;
    
    private List<UIInventoryItem> uiItems;


    private void Awake()
    {
        Inventory.OnInventoryChaned += UpdateInventoryUI;
        uiItems = new List<UIInventoryItem>();
    }

    /// <summary>
    /// When in inventory is changing something we need to update our UI elements
    /// </summary>
    /// <param name="items"></param>
    private void UpdateInventoryUI(List<InventoryItem> items)
    {
        while(uiItems.Count < items.Count) CreateUIItem();

        for (int i = 0, j = items.Count; i < uiItems.Count; i++)
        {
            if (i < j)
            {
                uiItems[i].GetGameObject().SetActive(true);
                uiItems[i].ChangeItemUI(items[i].plantInformation, items[i].amount);
            }
            else
            {
                uiItems[i].GetGameObject().SetActive(false);   
            }
        }
    }

    private void CreateUIItem()
    {
        GameObject ui = Instantiate(uiItemPrefab, parentToAddPrefab.transform);
        uiItems.Add(ui.GetComponent<UIInventoryItem>());
    }

    private void OnEnable()
    {
        Inventory.OnInventoryChaned += UpdateInventoryUI;
        UpdateInventoryUI(Inventory.Instance.GetItems());
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChaned -= UpdateInventoryUI;
    }
}

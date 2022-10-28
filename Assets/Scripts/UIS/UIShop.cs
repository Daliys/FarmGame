using System;
using System.Collections.Generic;
using ScriptableObjects;
using UIS;
using UnityEngine;

public class UIShop : MonoBehaviour
{
    // information about pages
    [SerializeField] private ObjectInformation[] seedsItem;
    [SerializeField] private ObjectInformation[] animalsItem;
    [SerializeField] private ObjectInformation[] flowersItem;
    [SerializeField] private ObjectInformation[] neutralItem;

    //parent for shopItems
    [SerializeField] private GameObject itemsPanel;
    [SerializeField] private GameObject shopItemPrefab;
    
    [Serializable]
    public enum ShopPage
    {
        Seeds, Animals, Flowers, Neutral
    }

    private ShopPage _currentOpenShopPage = ShopPage.Seeds;

    // List with all GameObject of ShopItem (saving here for reusing when changing tabs)
    private List<GameObject> _itemsList;

    private void Awake()
    {
        _itemsList = new List<GameObject>();
    }

    /// <summary>
    /// Action When User Clicked the Category Button
    /// </summary>
    /// <param name="shopPage">What Type of page we need to load</param>
    public void OnButtonPageClicked(ShopCategoryEnum shopPage)
    {
        _currentOpenShopPage = shopPage.currentShopPage;
        UpdateItemsPanel();
    }

    private void UpdateItemsPanel()
    {
        var objectsCount = GetCurrentObjectInformation().Length;
     
        for (var i = 0; i < Math.Max(_itemsList.Count, objectsCount); i++)
        { 
            if (i < objectsCount)
            {
                if(i >= _itemsList.Count) CreateShopItem();
               
                _itemsList[i].gameObject.SetActive(true);
                _itemsList[i].GetComponent<UIShopItem>().InitializeShopItem(GetCurrentObjectInformation()[i]);
            }
            else
            {
                _itemsList[i].gameObject.SetActive(false);
            }
        }
    }

    private void CreateShopItem()
    {
        _itemsList.Add(Instantiate(shopItemPrefab, itemsPanel.transform));
    }

    private ObjectInformation[] GetCurrentObjectInformation()
    {
        return _currentOpenShopPage switch
        {
            ShopPage.Seeds => seedsItem,
            ShopPage.Animals => animalsItem,
            ShopPage.Flowers => flowersItem,
            ShopPage.Neutral => neutralItem,
            _ => Array.Empty<ObjectInformation>()
        };
    }

    private void OnEnable()
    {
        UpdateItemsPanel();
    }
}

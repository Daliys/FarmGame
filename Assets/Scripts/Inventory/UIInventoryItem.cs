using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    //ref to UI elements
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image itemImage;

    private GameObject _parentGameObject;
    
    private PlantInformation _plantInformation;

    private void Awake()
    {
        _parentGameObject = gameObject;
    }

    /// <summary>
    /// Changing UI element (amount, price, name, plantImage)
    /// </summary>
    /// <param name="plantInformation">Information about plant</param>
    /// <param name="amount">amount we have on our Inventory</param>
    public void ChangeItemUI(PlantInformation plantInformation, int amount)
    {
        _plantInformation = plantInformation;

        nameText.text = plantInformation.itemName;
        countText.text = "x " + amount;
        priceText.text = "$" + plantInformation.marketPrice.ToString();
        itemImage.sprite = plantInformation.uiSprite;
    }

    public GameObject GetGameObject()
    {
        return _parentGameObject;
    }
}

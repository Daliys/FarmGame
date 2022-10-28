using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private Button clickableButton;
    [SerializeField] private Color availableToBuyPriceColor;
    [SerializeField] private Color unavailableToBuyPriceColor;
    private ObjectInformation _objectInformation;

    public void InitializeShopItem(ObjectInformation objectInformation)
    {
        _objectInformation = objectInformation;
        itemNameText.text = objectInformation.itemName;
        itemImage.sprite = objectInformation.uiSprite;
        itemPriceText.text = objectInformation.price + "$";
        
        OnMoneyChanged(REF.Instance.Game.GetCurrentMoney());
    }

    public void OnButtonClicked()
    {
        REF.Instance.PurchaseProcessing.ProcessBuying(_objectInformation);
        REF.Instance.UI.CloseAllPanels();
    }

    private void OnEnable()
    {
        Game.OnMoneyChanged += OnMoneyChanged;
        
    }

    private void OnDisable()
    {
        Game.OnMoneyChanged -= OnMoneyChanged;
    }

    /// <summary>
    /// Actions when count of money changed
    /// </summary>
    /// <param name="count">current count of money</param>
    private void OnMoneyChanged(int count)
    {
        if (count >= _objectInformation.price)
        {
            clickableButton.enabled = true;
            itemPriceText.color = availableToBuyPriceColor;
        }
        else
        {
            clickableButton.enabled = false;
            itemPriceText.color = unavailableToBuyPriceColor;
        }
        
    }
}

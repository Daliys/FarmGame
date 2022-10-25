using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemPrice;
    private ObjectInformation _objectInformation;

    public void InitializeShopItem(ObjectInformation objectInformation)
    {
        _objectInformation = objectInformation;
        itemName.text = objectInformation.itemName;
        itemImage.sprite = objectInformation.uiSprite;
        itemPrice.text = objectInformation.price + "$";
    }

    public void OnButtonClicked()
    {
        REF.Instance.PurchaseProcessing.ProcessBuying(_objectInformation);
    }
}

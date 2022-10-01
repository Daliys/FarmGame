using UnityEngine;

public class UIShop : MonoBehaviour
{
    [SerializeField] private PurchaseProcessing purchaseProcessing;
    
    /**
     * Processing buying item
     */
    public void OnButtonBuyClicked(ShopItemScriptableObject item)
    {
        purchaseProcessing.ProcessBuying(item);
    }
}

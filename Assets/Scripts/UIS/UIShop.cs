using ScriptableObjects;
using UnityEngine;

public class UIShop : MonoBehaviour
{
    [SerializeField] private PurchaseProcessing purchaseProcessing;
    
    /**
     * Processing buying item
     */
    public void OnButtonBuyClicked(ObjectInformation item)
    {
        purchaseProcessing.ProcessBuying(item);
    }
 
}

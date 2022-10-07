using UnityEngine;

public class PurchaseProcessing : MonoBehaviour
{
    [SerializeField] private MouseControl mouseControl;
    private ShopItemScriptableObject _currentItem;
    public void ProcessBuying(ShopItemScriptableObject item)
    {
        if (Game.Instance.IsEnoughMoneyToBuy(item.cost))
        {
            _currentItem = item;
            //itemGameObject = Instantiate(item.prefab);
            //gameObject.name = item.itemName;
            
            mouseControl.AddFollowingMouseItem(PurchaseItem);
        }
    }

    /// <summary>
    /// Trying to buy item
    /// </summary>
    private bool PurchaseItem(RaycastHit hit)
    {
        if (!hit.collider.CompareTag(Tags.Garden)) return false;

        Garden garden = hit.collider.GetComponent<Garden>();
        
        if (garden.IsHavePlant) return false;

        bool isSuccessful = Game.Instance.PurchaseItem(_currentItem.cost);
        
        if (isSuccessful)
        {
            garden.SetSeed(_currentItem.plantInformation);
        }
        else
        {
            return false;
        }
        
        _currentItem = null;
        
        return true;
        
    }
}

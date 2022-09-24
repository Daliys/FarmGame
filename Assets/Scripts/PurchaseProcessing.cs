using UnityEngine;

public class PurchaseProcessing : MonoBehaviour
{
    [SerializeField] private MouseControl mouseControl;
    private ShopItemScriptableObject _currentItem;
    private GameObject itemGameObject;
    public void ProcessBuying(ShopItemScriptableObject item)
    {
        if (Game.Instance.IsEnoughMoneyToBuy(item.cost))
        {
            _currentItem = item;
            itemGameObject = Instantiate(item.prefab);
            gameObject.name = item.itemName;
            
            mouseControl.AddFollowingMouseItem(itemGameObject, PurchaseItem);
        }
    }

    /// <summary>
    /// Trying to buy item
    /// </summary>
    private void PurchaseItem()
    {
        bool isSuccessful = Game.Instance.PurchaseItem(_currentItem.cost);
        if (isSuccessful)
        {
            BoxCollider boxCollider = itemGameObject.GetComponent<BoxCollider>();
            if (boxCollider != null) boxCollider.enabled = true;
        }
        else
        {
            Destroy(itemGameObject); 
        }

        itemGameObject = null;
        _currentItem = null;
    }
}

using ScriptableObjects;
using UnityEngine;

public class PurchaseProcessing : MonoBehaviour
{
    [SerializeField] private MouseControl mouseControl;
    private PlantInformation _currentItem;
    public void ProcessBuying(PlantInformation item)
    {
        if (GameReferences.Instance.Game.IsEnoughMoneyToBuy(item.seedPrice))
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

        bool isSuccessful = GameReferences.Instance.Game.PurchaseItem(_currentItem.seedPrice);
        
        if (isSuccessful)
        {
            garden.SetSeed(_currentItem);
        }
        else
        {
            return false;
        }
        
        _currentItem = null;
        
        return true;
        
    }
}

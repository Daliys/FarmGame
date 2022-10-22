using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class PurchaseProcessing : MonoBehaviour
{
    [FormerlySerializedAs("mouseControl")] [SerializeField] private InputController inputController;
    private PlantInformation _currentItem;
    public void ProcessBuying(PlantInformation item)
    {
        if (REF.Instance.Game.IsEnoughMoneyToBuy(item.seedPrice))
        {
            _currentItem = item;
            //itemGameObject = Instantiate(item.prefab);
            //gameObject.name = item.itemName;
            
            inputController.AddFollowingMouseItem(PurchaseItem);
        }
    }

    /// <summary>
    /// Trying to buy item
    /// </summary>
    private bool PurchaseItem(RaycastHit hit)
    {
        if (!hit.collider.CompareTag(Tags.Garden)) return false;

        Garden garden = hit.collider.gameObject.GetComponentInParent<Garden>();
        
        if (garden.IsHavePlant) return false;

        bool isSuccessful = REF.Instance.Game.PurchaseItem(_currentItem.seedPrice);
        
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

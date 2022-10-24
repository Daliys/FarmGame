using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PurchaseProcessing : MonoBehaviour
{
    [FormerlySerializedAs("mouseControl")] [SerializeField]
    private InputController inputController;

    private ObjectInformation _currentItem;

    public void ProcessBuying(ObjectInformation item)
    {
        if (!REF.Instance.Game.IsEnoughMoneyToBuy(item.price)) return;

        _currentItem = item;
        if (item is PlantInformation)
        {
            inputController.AddFollowingMouseItem(PurchaseItem);
        }
        else
        {
            GameObject gm = Instantiate(item.prefab);
            inputController.AddFollowingMouseItem(PurchaseItem, gm);
        }
    }
    
    /// <summary>
    /// Trying to buy item
    /// </summary>
    private bool PurchaseItem(RaycastHit hit)
    {
        if (_currentItem is PlantInformation) return PurchasePlantItem(hit);

        return PurchaseObjectItem(hit);
    }


    private bool PurchasePlantItem(RaycastHit hit)
    {
        if (!hit.collider.CompareTag(Tags.Garden)) return false;

        Garden garden = hit.collider.gameObject.GetComponentInParent<Garden>();

        if (garden.IsHavePlant) return false;

        bool isSuccessful = REF.Instance.Game.PurchaseItem(_currentItem.price);
        
        if (!isSuccessful) return false;
        
        garden.SetSeed((PlantInformation)_currentItem);
        _currentItem = null;

        return true;
    }

    private bool PurchaseObjectItem(RaycastHit hit)
    {
        return true;
    }
}

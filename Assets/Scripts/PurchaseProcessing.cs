using ScriptableObjects;
using UnityEngine;

public class PurchaseProcessing : MonoBehaviour
{
    [SerializeField] private InputManagerController inputController;

    private ObjectInformation _currentItem;

    public void ProcessBuying(ObjectInformation item)
    {
        _currentItem = item;
        if (item is PlantInformation)
        {
            inputController.AddFollowingMouseItem(PurchaseItem);
        }
        else
        {
            inputController.AddFollowingMouseItem(PurchaseItem, item.prefab);
        }
    }

    /// <summary>
    /// Callback for buying item
    /// </summary>
    private bool PurchaseItem(RaycastHit hit)
    {
        if (_currentItem is PlantInformation) return PurchasePlantItem(hit);

        return PurchaseObjectItem(hit);
    }

    /// <summary>
    /// Callback for buying plant 
    /// </summary>
    private bool PurchasePlantItem(RaycastHit hit)
    {
        if (!hit.collider.CompareTag(Tags.Garden)) return false;

        Garden garden = hit.collider.gameObject.GetComponentInParent<Garden>();

        if (garden.IsHavePlant) return false;

        bool isSuccessful = REF.Instance.Game.PurchaseItem(_currentItem.price);

        if (!isSuccessful) return false;

        garden.SetSeed((PlantInformation)_currentItem);

        return true;
    }

    /// <summary>
    /// Callback for buying object 
    /// </summary>
    private bool PurchaseObjectItem(RaycastHit hit)
    {
        return REF.Instance.Game.PurchaseItem(_currentItem.price);
    }
}

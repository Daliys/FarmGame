using System.Collections;
using ScriptableObjects;
using UnityEngine;

public class Garden : MonoBehaviour
{
    /// <summary>
    /// The Balloon that show up to the Garden when the Plant need some cure or care
    /// </summary>
    [SerializeField]private GardenBalloon gardenBalloon;

    /// <summary>
    /// Information about a planted plant
    /// </summary>
    private PlantInformation _plantInformation;

    /// <summary>
    /// Stages of plant
    /// </summary>
    enum GardenStatus
    {
        Growing, Watering, Harvesting
    }

    private GardenStatus _currentStatus;

    private GameObject _seedGameObject;
    
    /// <summary>
    /// plan the seen on the garden
    /// </summary>
    /// <param name="plantInformation">Seed that we need to seed</param>
    public void SetSeed(PlantInformation plantInformation)
    {
        _plantInformation = plantInformation;
        _currentStatus = GardenStatus.Growing;
        
        _seedGameObject = Instantiate(_plantInformation.prefab, transform, true);
        _seedGameObject.transform.localPosition = new Vector3(0, 1.5f, 0);
        RequestWatering();
    }

    /// <summary>
    /// Showing Balloon that we need to water this garden
    /// </summary>
    private void RequestWatering()
    {
        _currentStatus = GardenStatus.Watering;
        gardenBalloon.ShowBalloon(GardenBalloon.IconType.Watering, OnButtonClicked);
    }
    
    /// <summary>
    /// Showing Balloon that we need to harvest this garden
    /// </summary>
    private void RequestHarvesting()
    {
        _currentStatus = GardenStatus.Harvesting;
        gardenBalloon.ShowBalloon(GardenBalloon.IconType.Harvesting, OnButtonClicked);
        
    }

    /// <summary>
    /// Action when NavMeshAgent Came to the Garden and starting Watering 
    /// </summary>
    public void StartWatering()
    {
        // Doing some watering 

        StartCoroutine(WaitBeforeHarvest());
    }

    /// <summary>
    /// Action when NavMeshAgent Came to the Garden and starting Harvesting 
    /// </summary>
    public InventoryItem StartHarvesting()
    {
        // Doing some harvesting 
        InventoryItem inventoryItem = new InventoryItem(_plantInformation, Random.Range(1, 5));
        
        Destroy(_seedGameObject);
        _plantInformation = null;

        return inventoryItem;
    }

    /// <summary>
    /// CallBack that calls When the Balloon has been clicked
    /// </summary>
    private void OnButtonClicked()
    {
        switch (_currentStatus)
        {
            case GardenStatus.Watering:
                GameReferences.Instance.TasksManager.AddWateringAction(this);
                break;
            case GardenStatus.Harvesting:
                GameReferences.Instance.TasksManager.AddHarvestingAction(this);
                break;
        }
    }

    //FIXME Change this logic to something else
    private IEnumerator WaitBeforeHarvest()
    {
        yield return new WaitForSeconds(3);
        RequestHarvesting();
    }
    
    /// <summary>
    /// If this garden already have a plant
    /// </summary>
    public bool IsHavePlant => _plantInformation;
}

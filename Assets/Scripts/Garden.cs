using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Inventories;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Garden : MonoBehaviour
{
    /// <summary>
    /// The Balloon that show up to the Garden when the Plant need some cure or care
    /// </summary>
    [SerializeField]private GardenBalloon gardenBalloon;

    [FormerlySerializedAs("pointForSeed")] [SerializeField] private List<Transform> pointsForSeed;

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

    private List<GameObject> _plantGameObjects;


    private void Awake()
    {
        _plantGameObjects = new List<GameObject>();
    }

    /// <summary>
    /// plan the seen on the garden
    /// </summary>
    /// <param name="plantInformation">Seed that we need to seed</param>
    public void SetSeed(PlantInformation plantInformation)
    {
        _plantInformation = plantInformation;
        _currentStatus = GardenStatus.Growing;

       
        for (int i = 0; i < pointsForSeed.Count; i++)
        {
            GameObject gm = Instantiate(_plantInformation.prefab, transform);
            gm.transform.position = pointsForSeed[i].position;
            _plantGameObjects.Add(gm);
        }
        
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
    public void StartHarvesting(PlayerInventory inventoryToAdd)
    {
        if(inventoryToAdd.IsInventoryFull()) return;

        // Doing some harvesting 
        InventoryItem inventoryItem = new InventoryItem(_plantInformation, 1);

        foreach (var plant in _plantGameObjects)
        {
            Destroy(plant);
        }
        _plantGameObjects.Clear();
        
        _plantInformation = null;

        inventoryToAdd.AddItem(inventoryItem);
    }

    /// <summary>
    /// CallBack that calls When the Balloon has been clicked
    /// </summary>
    private void OnButtonClicked()
    {
        switch (_currentStatus)
        {
            case GardenStatus.Watering:
                REF.Instance.TasksManager.AddWateringAction(this);
                break;
            case GardenStatus.Harvesting:
                REF.Instance.TasksManager.AddHarvestingAction(this);
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

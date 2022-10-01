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
    private SeedInformation _seedInformation;
   
    public void SetSeed(SeedInformation seedInformation)
    {
        _seedInformation = seedInformation;
        
        GameObject seedGameObject = Instantiate(_seedInformation.prefab, transform, true);
        seedGameObject.transform.localPosition = new Vector3(0, 1.5f, 0);
        RequestWatering();
    }

    /// <summary>
    /// Showing Balloon that we need to water this garden
    /// </summary>
    private void RequestWatering()
    {
        gardenBalloon.ShowWateringBalloon(OnButtonClicked);
    }

    /// <summary>
    /// Action when NavMeshAgent Came to the Garden and starting Watering 
    /// </summary>
    public void StartWatering()
    {
        // Doing some watering 
    }

    /// <summary>
    /// CallBack that calls When the Balloon has been clicked
    /// </summary>
    private void OnButtonClicked()
    {
        TasksManager.Instance.AddWateringAction(this);
    }
    
    /// <summary>
    /// If this garden already have a plant
    /// </summary>
    public bool IsHaveSeed => _seedInformation;
}

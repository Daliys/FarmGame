using ScriptableObjects;
using UnityEngine;

public class Garden : MonoBehaviour
{
    private SeedInformation _seedInformation;

    public void setSeed(SeedInformation seedInformation)
    {
        _seedInformation = seedInformation;
        
        GameObject gameObject = Instantiate(_seedInformation.prefab);gameObject.transform.parent = transform;
        gameObject.transform.localPosition = new Vector3(0, 1.5f, 0);
        
    }
    
    public bool IsHaveSeed => _seedInformation;
}

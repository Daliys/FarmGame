using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SeedInformation", menuName = "ScriptableObjects/SeedInformation")]
    public class SeedInformation : ScriptableObject
    {
        public GameObject prefab;
    
        public float grownStepTime;
    
        
    }
}

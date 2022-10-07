using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlantInformation", menuName = "ScriptableObjects/PlantInformation")]
    public class PlantInformation : ScriptableObject
    {
        public enum Rarity
        {
            Normal, Super
        }
        
        public GameObject prefab;
    
        public float grownStepTime;
        
        public Rarity rarity;
        
        public int marketPrice;

    }
}
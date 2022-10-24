using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlantInformation", menuName = "ScriptableObjects/PlantInformation")]
    public class PlantInformation : ObjectInformation
    {
        public enum Rarity
        {
            Normal, Super
        }
        
   
        public float grownStepTime;
        
        public Rarity rarity;
        
        public int marketPrice;



    }
}

using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ObjectInformation", menuName = "ScriptableObjects/ObjectInformation")]
    public class ObjectInformation : ScriptableObject
    {
        public GameObject prefab;

        public int price;

        public string itemName;
    
        public string description;

        public Sprite uiSprite;
    }
}
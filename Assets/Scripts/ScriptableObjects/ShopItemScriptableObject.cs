using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/ShopItem")]
public class ShopItemScriptableObject : ScriptableObject
{
    [FormerlySerializedAs("seedInformationScriptableObject")] public SeedInformation seedInformation;
    
    public int cost;

    public string itemName;
    
    public string description;
    
    
}

using ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/ShopItem")]
public class ShopItemScriptableObject : ScriptableObject
{
    public PlantInformation plantInformation;
    
    public int cost;

    public string itemName;
    
    public string description;
    
    
}

using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/ShopItem")]
public class ShopItemScriptableObject : ScriptableObject
{
    public GameObject prefab;
    
    public int cost;

    public string itemName;
    
    public string description;
    
    
}

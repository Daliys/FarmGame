using ScriptableObjects;

public class InventoryItem
{
    public readonly PlantInformation plantInformation;

    public int amount;

    public InventoryItem(PlantInformation plantInformation, int amount)
    {
        this.plantInformation = plantInformation;
        this.amount = amount;
    }
}

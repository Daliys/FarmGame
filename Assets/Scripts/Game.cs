using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /**
     * Player's current amount of money
     */
    [SerializeField] private int money;


    /// <summary>
    /// Checking and processing buying of item
    /// </summary>
    /// <param name="cost">Cost of item</param>
    /// <returns>is the purchase has been successful</returns>
    public bool PurchaseItem(int cost)
    {
        if (!IsEnoughMoneyToBuy(cost)) return false;
        
        money -= cost;
        return true;
    }

    /// <summary>
    /// Check is it possible to buy item
    /// </summary>
    /// <param name="cost">Cost of the item</param>
    /// <returns>Is it enough money to buy the item</returns>
    public bool IsEnoughMoneyToBuy(int cost)
    {
        return money >= cost;
    }

}

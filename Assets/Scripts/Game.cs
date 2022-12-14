using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    /**
     * Action calls when amount of money changing
     */
    public static event Action<int> OnMoneyChanged;
    
    /**
     * Player's current amount of money
     */
    [SerializeField] private int money;

    private bool _isPaused = false;

    


    private void Awake()
    {
        OnMoneyChanged?.Invoke(money);
    }
    
    /// <summary>
    /// Checking and processing buying of item
    /// </summary>
    /// <param name="cost">Cost of item</param>
    /// <returns>is the purchase has been successful</returns>
    public bool PurchaseItem(int cost)
    {
        if (!IsEnoughMoneyToBuy(cost)) return false;
        
        money -= cost;
        OnMoneyChanged?.Invoke(money);
        
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

    public int GetCurrentMoney()
    {
        return money;
    }
    
    
    public bool IsPaused
    {
        get => _isPaused;
        set
        {
            _isPaused = value;
            Time.timeScale = value? 0 : 1;
        }
    }
}

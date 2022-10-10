using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private GameObject dayPanel;

    [SerializeField] private GameObject pausePanel;

    [SerializeField] private TextMeshProUGUI amountOfMoneyText;
    
    [SerializeField] private GameObject storeHousePanel;

    private void OnEnable()
    {
        Game.OnMoneyChanged += OnAmountOfMoneyChanged;
    }

    private void OnDisable()
    {
        Game.OnMoneyChanged -= OnAmountOfMoneyChanged;
    }

    public void OnSettingButtonClicked()
    {
        dayPanel.SetActive(pausePanel.activeSelf);
        pausePanel.SetActive(!pausePanel.activeSelf);
        
        shopPanel.SetActive(false);
        storeHousePanel.SetActive(false);
    }

    public void OnShopButtonClicked()
    {
        dayPanel.SetActive(true);
        shopPanel.SetActive(!shopPanel.activeSelf);
        
        storeHousePanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void OnStoreHouseButtonClicked()
    {
        dayPanel.SetActive(true);
        storeHousePanel.SetActive(!storeHousePanel.activeSelf);
        
        shopPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    
    public void OnPauseButtonClicked()
    {
        
    }

    private void OnAmountOfMoneyChanged(int currentAmount)
    {
        amountOfMoneyText.text = currentAmount.ToString();
    }



}

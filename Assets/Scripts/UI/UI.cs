using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private GameObject dayPanel;

    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Text amountOfMoneyText;

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
        shopPanel.SetActive(false);
        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    public void OnShopButtonClicked()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        dayPanel.SetActive(true);
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

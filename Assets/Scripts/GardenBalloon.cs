using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Canvas what shows when garden need some care or cure and allow player to click on the button to start action
/// </summary>
public class GardenBalloon : MonoBehaviour
{
    [SerializeField] private UIBalloonImages uiBalloonImages; 
    [SerializeField] private Image icon;
    [SerializeField] private GameObject canvas;

    /// <summary>
    /// CallBack Action when Button clicked
    /// </summary>
    private Action _onButtonClickedAction; 
    
    private void ChangeImageToWatering()
    {
        icon.sprite = uiBalloonImages.watering;
    }

    /// <summary>
    /// Calls when player clicking on the button to start Action
    /// </summary>
    public void OnButtonClicked()
    {
        _onButtonClickedAction?.Invoke();
        canvas.SetActive(false);
    }

    /// <summary>
    /// Showing UI Button with Watering Icon
    /// </summary>
    /// <param name="onButtonClicked">CallBack action when button is clicked</param>
    public void ShowWateringBalloon(Action onButtonClicked)
    {
        _onButtonClickedAction = onButtonClicked;
        canvas.SetActive(true);
        ChangeImageToWatering();
    }

}

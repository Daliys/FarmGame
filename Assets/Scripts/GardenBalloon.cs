using System;
using ScriptableObjects;
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
    /// Type of Icon to use for Button Image
    /// </summary>
    public enum IconType
    {
        Watering,
        Harvesting
    }

    /// <summary>
    /// CallBack Action when Button clicked
    /// </summary>
    private Action _onButtonClickedAction;

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
    /// <param name="iconType">Type of Icon that we need to show</param>
    /// <param name="onButtonClicked">CallBack action when button is clicked</param>
    public void ShowBalloon(IconType iconType, Action onButtonClicked)
    {
        _onButtonClickedAction = onButtonClicked;
        canvas.SetActive(true);

        icon.sprite = iconType switch
        {
            IconType.Watering => uiBalloonImages.watering,
            IconType.Harvesting => uiBalloonImages.harvesting,
            _ => icon.sprite
        };
    }
}
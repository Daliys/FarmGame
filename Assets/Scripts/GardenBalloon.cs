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
    [SerializeField] private GameObject uiPrefab;
    private GameObject _canvas;
    private GameObject _uiGameObject;
    private Camera _mainCamera;


    private void Start()
    {
        _canvas = REF.Instance.WorldObjectsCanvas;
        _mainCamera = Camera.main;
    }

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
        Destroy(_uiGameObject);
    }

    /// <summary>
    /// Showing UI Button with Watering Icon
    /// </summary>
    /// <param name="iconType">Type of Icon that we need to show</param>
    /// <param name="onButtonClicked">CallBack action when button is clicked</param>
    public void ShowBalloon(IconType iconType, Action onButtonClicked)
    {
        _onButtonClickedAction = onButtonClicked;
        _canvas.SetActive(true);

        _uiGameObject = Instantiate(uiPrefab, _canvas.transform);
        _uiGameObject.GetComponent<Image>().sprite = iconType switch
        {
            IconType.Watering => uiBalloonImages.watering,
            IconType.Harvesting => uiBalloonImages.harvesting,
        };

        _uiGameObject.GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }
    
    private void LateUpdate()
    {
        if (_uiGameObject)
        {
            _uiGameObject.transform.position = _mainCamera.WorldToScreenPoint(transform.position);
        }
    }

}
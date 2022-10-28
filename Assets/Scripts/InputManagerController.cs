using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class InputManagerController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    /**
     * Reference to Camera <see cref="Camera"/>
     */
    private Camera _mainCamera;

    /**
     * Action of buying item from shop. It's invoke after mouseClicked
     */
    private Func<RaycastHit, bool> _actionForBuyingItems;

    /**
     * Action, mouse is clicked 
     */
    public static event Action<RaycastHit> OnMouseButtonClicked;

    /// <summary>
    /// Ref to CursorChanger
    /// </summary>
    private CursorChanger _cursorChanger;

    private GameObject _followingGameObject;

    private bool _isAvailableToPlace;

    private void Start()
    {
        _mainCamera = GetComponent<Camera>();
        _cursorChanger = GetComponent<CursorChanger>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckMouseButtonClicked();
        OnMouseChangedPosition();
    }

    /// <summary>
    /// Processing mouse clicked 
    /// </summary>
    private void OnMouseClicked(RaycastHit hit)
    {
        if (_actionForBuyingItems != null)
        {
            if (!_isAvailableToPlace) return;

            if (_actionForBuyingItems.Invoke(hit))
            {
                if (_followingGameObject)
                {
                    GameObject gm = Instantiate(_followingGameObject);
                    _followingGameObject = gm;
                }
            }
            else
            {
                if(_followingGameObject) Destroy(_followingGameObject);
                ClenBuyingRef();  
            }
            
        }
        else
        {
            OnMouseButtonClicked?.Invoke(hit);
            
            // Close All open UI windows when set a new MovementTask
            if (REF.Instance.UI.IsAnyPanelOpen())
            {
                REF.Instance.UI.CloseAllPanels();
            }
        }
    }

    private void CheckMouseButtonClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    OnMouseClicked(hit);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CancelAction();
        }
    }

    private void OnMouseChangedPosition()
    {
        if (!_followingGameObject) return;

        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
        {
            Vector3 newPosition = MathUtil.RoundTo(raycastHit.point, 0.5f);
            _followingGameObject.transform.position = newPosition;

            OverlapChecker overlapChecker = _followingGameObject.GetComponent<OverlapChecker>();

            _isAvailableToPlace = overlapChecker.CanSpawnInLocation(newPosition);
            overlapChecker.ChangeMaterialAvailable(_isAvailableToPlace);
        }
    }

    public void AddFollowingMouseItem(Func<RaycastHit, bool> actionAfterMouseClick)
    {
        _actionForBuyingItems = actionAfterMouseClick;
        _cursorChanger.ChangeCursor(CursorChanger.CursorType.Seed);
        _isAvailableToPlace = true;
    }

    public void AddFollowingMouseItem(Func<RaycastHit, bool> actionAfterMouseClick, GameObject followingObject)
    {
        _followingGameObject = Instantiate(followingObject);
        _actionForBuyingItems = actionAfterMouseClick;
    }
    
    private void CancelAction()
    {
        // if we in buying mode 
        if (_actionForBuyingItems != null)
        {
            ClenBuyingRef();
        }
        // if any UI window open right now
        else if (REF.Instance.UI.IsAnyPanelOpen())
        {
            REF.Instance.UI.CloseAllPanels();
        }
        // cancel all task in taskManager 
        else
        {
            REF.Instance.TasksManager.CancelAllTask();
        }
    }

    /// <summary>
    /// Destroy and set null all variables related to buying information
    /// </summary>
    private void ClenBuyingRef()
    {
        if(_followingGameObject) Destroy(_followingGameObject);
        _actionForBuyingItems = null;
        _followingGameObject = null;
        _cursorChanger.ChangeCursor(CursorChanger.CursorType.Default);
    }
}

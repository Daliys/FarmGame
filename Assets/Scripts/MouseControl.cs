using System;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    /**
     * percent of window size in width, during pointing in this area the camera will move 
     */
    [SerializeField] private float cameraBorderWidthPercent;
    
    /**
     * percent of window size in height, during pointing in this area the camera will move
     */
    [SerializeField] private float cameraBorderHeightPercent; 
    
    /**
     * Reference to Camera <see cref="Camera"/>
     */
    private Camera _mainCamera;

    /**
     * Action, mouse is clicked 
     */
    public static event Action<RaycastHit> OnMouseButtonClicked;

    /**
     * Local variable of mouse position 
     */
    private Vector2 _mousePosition;

    /**
     * Local variable of current Window
     */
    private Vector2 _windowSize;

    /**
     * Speed of the camera movement when cursor in movementZone
     */
    [SerializeField]private float cameraMovementSpeed;
    
    /**
     * Action of buying item from shop. It's invoke after mouseClicked
     */
    private Func<RaycastHit, bool> _actionForBuyingItems;

    private CursorChanger _cursorChanger;
    
    void Start()
    {
        _mainCamera = GetComponent<Camera>();
        _windowSize = new Vector2(Screen.width, Screen.height);
        _cursorChanger = GetComponent<CursorChanger>();
    }

    // Update is called once per frame

    void Update()
    {
        //FIXME remove not using method
        //OnMouseChangedPosition();
        
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClicked();
        }
       
        CheckAndMoveCamera();
    }

    /// <summary>
    /// Processing mouse clicked 
    /// </summary>
    private void OnMouseClicked()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            if (_actionForBuyingItems != null)
            {
                if( _actionForBuyingItems.Invoke(hit))
                {
                    _actionForBuyingItems = null;
                    _cursorChanger.ChangeCursor(CursorChanger.CursorType.Default);
                }
            }
            else
            {
                OnMouseButtonClicked?.Invoke(hit);
            }
        }
    }

    private void OnMouseChangedPosition()
    {
        if (_actionForBuyingItems != null) return;

        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
         
        }
    }

    public void AddFollowingMouseItem(Func<RaycastHit, bool> actionAfterMouseClick)
    {
        _actionForBuyingItems = actionAfterMouseClick;
        _cursorChanger.ChangeCursor(CursorChanger.CursorType.Seed);
    }

    /**
     * Camera movement while cursor in the border area
     */
    private void CheckAndMoveCamera()
    {
        _mousePosition = Input.mousePosition;
        float mousePercentX = _mousePosition.x / _windowSize.x;
        float mousePercentY = _mousePosition.y / _windowSize.y;
        
        if (mousePercentX < cameraBorderWidthPercent)
        {
            MoveCamera(Vector3.left);
        }
        else if (mousePercentX > 1 - cameraBorderWidthPercent)
        {
            MoveCamera(Vector3.right);
        }
        
        if (mousePercentY < cameraBorderHeightPercent)
        {
            MoveCamera(Vector3.back);
        }
        else if (mousePercentY > 1 - cameraBorderHeightPercent)
        {
            MoveCamera(Vector3.forward);
        }
    }

    /// <summary>
    /// Moving camera in the specified direction
    /// </summary>
    /// <param name="vector"> direction to move </param>
    private void MoveCamera(Vector3 vector)
    {
        float y = transform.position.y;
        transform.Translate( Time.deltaTime * cameraMovementSpeed * vector, Space.Self);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}    

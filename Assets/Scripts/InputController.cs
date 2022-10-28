using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    /**
     * percent of window size in width, during pointing in this area the camera will move 
     */
    [SerializeField] private float cameraBorderWidthPercent;

    /**
     * percent of window size in height, during pointing in this area the camera will move
     */
    [SerializeField] private float cameraBorderHeightPercent;

    /// <summary>
    /// If it is true then move camera by mouse wont be available
    /// </summary>
    [SerializeField] private bool lockMouseMovement;

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
    [SerializeField] private float cameraMovementSpeed;

    /// <summary>
    /// Speed of the camera zoom
    /// </summary>
    [SerializeField] private float cameraZoomSpeed;

    /// <summary>
    /// Borders of the camera zoom.
    /// [x - zoom in border; y - zoom out border]
    /// </summary>
    [SerializeField] private Vector2 borderOfCameraZoom;

    /// <summary>
    /// Current zoom - using to control zoom borders
    /// </summary>
    private float _currentZoom;

    private void Start()
    {
        _windowSize = new Vector2(Screen.width, Screen.height);
        _currentZoom = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //FIXME remove not using method
        //OnMouseChangedPosition();

        MouseZooming();

        if (!lockMouseMovement) CheckAndMoveCameraByMouse();

        CheckAndMoveCameraByKeyboard();
    }

    /// <summary>
    /// Changing zoom if player scrolling wheel
    /// </summary>
    private void MouseZooming()
    {
        var mouseWheels = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheels != 0)
        {
            if (_currentZoom + mouseWheels < borderOfCameraZoom.x && _currentZoom + mouseWheels > borderOfCameraZoom.y)
            {
                _currentZoom += mouseWheels;
                transform.Translate(mouseWheels * cameraZoomSpeed * Vector3.forward);
            }
        }
    }

    /**
     * Camera movement while cursor in the border area
     */
    private void CheckAndMoveCameraByMouse()
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

    /**
     * Camera movement is player using keybord movements
     */
    private void CheckAndMoveCameraByKeyboard()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontal *= 2;
            vertical *= 2;
        }

        MoveCamera(new Vector3(horizontal, 0, vertical));
    }
    
    /// <summary>
    /// Moving camera in the specified direction
    /// </summary>
    /// <param name="vector"> direction to move </param>
    private void MoveCamera(Vector3 vector)
    {
        float y = transform.position.y;
        transform.Translate(Time.deltaTime * cameraMovementSpeed * vector, Space.Self);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
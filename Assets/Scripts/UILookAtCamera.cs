using UnityEngine;

/// <summary>
/// Turning the UI up to the camera
/// </summary>
public class UILookAtCamera : MonoBehaviour
{
    private Camera _mainCamera;
 
    void Start()
    {
        _mainCamera = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.back,
            rotation * Vector3.up);
    }
}

using ScriptableObjects;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] private CursorImages cursorImages;
    
    /// <summary>
    /// Types of cursors that we have in the game
    /// </summary>
    public enum CursorType
    {
        Default,
        Seed
    }

    private void Awake()
    {
        ChangeCursor(CursorType.Default);
    }

    /// <summary>
    /// Changes cursor in the game
    /// </summary>
    /// <param name="cursorType">Type of cursor that we need to change</param>
    public void ChangeCursor(CursorType cursorType)
    {
        Texture2D texture;
        switch (cursorType)
        {
            case CursorType.Default:
                texture = cursorImages.defaultCursor;
                break;
            case CursorType.Seed:
                texture = cursorImages.seedCursor;
                break;
            default:
                texture = cursorImages.defaultCursor;
                break;
        }

        Cursor.SetCursor(texture, Vector2.zero, CursorMode.ForceSoftware);

    }
}

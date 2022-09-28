using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] private Texture2D seedCursor;
    [SerializeField] private Texture2D defaultCursor;

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
                texture = defaultCursor;
                break;
            case CursorType.Seed:
                texture = seedCursor;
                break;
            default:
                texture = defaultCursor;
                break;
        }

        Cursor.SetCursor(texture, Vector2.zero, CursorMode.ForceSoftware);

    }
}

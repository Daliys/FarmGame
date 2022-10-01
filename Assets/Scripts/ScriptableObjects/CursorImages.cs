using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CursorImages", menuName = "ScriptableObjects/CursorImages")]
    public class CursorImages : ScriptableObject
    {
        public Texture2D seedCursor;
        public Texture2D defaultCursor;
    }
}
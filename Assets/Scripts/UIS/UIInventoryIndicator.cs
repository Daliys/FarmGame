using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryIndicator : MonoBehaviour
{
    [SerializeField] private Sprite emptyImage;
    [SerializeField] private Sprite fullImage;
    [SerializeField] private Image image;

    public void ChangeToFull()
    {
        image.sprite = fullImage;
    }

    public void ChangeToEmpty()
    {
        image.sprite = emptyImage;
    }
    
}

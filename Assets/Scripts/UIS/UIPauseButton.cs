using UnityEngine;
using UnityEngine.UI;

namespace UIS
{
    public class UIPauseButton : MonoBehaviour
    {
        [SerializeField] private Image imageToChange;
        [SerializeField] private Sprite pauseSprite;
        [SerializeField] private Sprite playSprite;


        public void OnButtonClicked()
        {
            bool isPaused = !REF.Instance.Game.IsPaused;
            REF.Instance.Game.IsPaused = isPaused;

            imageToChange.sprite = isPaused ? pauseSprite : playSprite;
        }
        
    }
}
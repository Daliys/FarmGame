using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIS
{
    public class UIClockUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textView;
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;
        
        public void SetEnableSkipButton(bool isEnable)
        {
            button.enabled = isEnable;
            
            var colorBlock = button.colors;
            buttonImage.color = isEnable ? colorBlock.normalColor:colorBlock.disabledColor;
        }
        
        public void UpdateClockTime(string time)
        {
            textView.text = time;
        }

        public void OnButtonSkipClicked()
        {
            REF.Instance.DayTimer.SkipDay();
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOLVR.UI
{
    public class UISliderParseValue : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textUI;

        private void Start()
        {
            if (transform.parent.TryGetComponent(out Slider slider))
            {
                SetText(slider.value);
            }
        }

        public void SetText(float value) => textUI.text = value.ToString("0.00");
    }
}

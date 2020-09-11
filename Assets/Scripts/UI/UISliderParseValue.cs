using TMPro;
using UnityEngine;

namespace LOLVR.UI
{
    public class UISliderParseValue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textUI;

        public void SetText(float value) => textUI.text = value.ToString("0.00");
    }
}
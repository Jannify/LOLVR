using Edwon.VR;
using Edwon.VR.Gesture;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOLVR.UI
{
    public class SettingsUILoader : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown leftClickDropdown, rightClickDropdown, championDropdown, mainHandDropdown;
        [SerializeField] private Slider monitorSizeSlider;

        private void Start()
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            leftClickDropdown.value = leftClickDropdown.options.IndexOf(leftClickDropdown.options.FirstOrDefault(option => option.text == ConfigManager.LeftClick.ToString()));
            rightClickDropdown.value = rightClickDropdown.options.IndexOf(rightClickDropdown.options.FirstOrDefault(option => option.text == ConfigManager.RightClick.ToString()));
            mainHandDropdown.value = mainHandDropdown.options.IndexOf(mainHandDropdown.options.FirstOrDefault(option => option.text == ConfigManager.MainHand.ToString()));
            championDropdown.value = championDropdown.options.IndexOf(championDropdown.options.FirstOrDefault(option => option.text == ConfigManager.Champion));
            monitorSizeSlider.value = ConfigManager.MonitorSize;
        }

        public void SaveSettings()
        {
            Enum.TryParse(leftClickDropdown.options[leftClickDropdown.value].text, out VRKeyCodes leftClick);
            Enum.TryParse(rightClickDropdown.options[rightClickDropdown.value].text, out VRKeyCodes rightClick);
            Enum.TryParse(mainHandDropdown.options[mainHandDropdown.value].text, out Handedness handedness);

            ConfigManager.LeftClick = leftClick;
            ConfigManager.RightClick = rightClick;
            ConfigManager.MainHand = handedness;
            ConfigManager.MonitorSize = (float)Math.Round((decimal)monitorSizeSlider.value, 2);
            ConfigManager.Champion = championDropdown.options[championDropdown.value].text;

            ConfigManager.Save();
        }
    }
}

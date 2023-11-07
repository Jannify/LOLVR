using Edwon.VR;
using System;
using System.Linq;
using LOLVR.Config;
using LOLVR.Enums;
using LOLVR.Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOLVR.UI
{
    public class SettingsUILoader : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown leftClickDropdown, rightClickDropdown, championDropdown, mainHandDropdown, potionKeyDropdown;
        [SerializeField] private Slider monitorSizeSlider;

        public static readonly Map<string, VRKeyCodes> VRKeyCodeNames = new()
        {
            { "Y Button", VRKeyCodes.LEFT_PRIMARY },
            { "X Button", VRKeyCodes.LEFT_SECONDARY },
            { "B Button", VRKeyCodes.RIGHT_PRIMARY },
            { "A Button", VRKeyCodes.RIGHT_SECONDARY },
            { "Left Thumbstick Click", VRKeyCodes.LEFT_AXIS_CLICK },
            { "Left Thumbstick Touch", VRKeyCodes.LEFT_AXIS_TOUCH },
            { "Right Thumbstick Click", VRKeyCodes.RIGHT_AXIS_CLICK },
            { "Right Thumbstick Touch", VRKeyCodes.RIGHT_AXIS_TOUCH }
        };

        public static readonly Map<string, Handedness> HandednessNames = new()
        {
            { "Left", Handedness.Left },
            { "Right", Handedness.Right },
        };

        public static readonly Map<string, KeyboardKeyCodes> KeyboardKeyCodesNames = new()
        {
            { "1 Key", KeyboardKeyCodes.N1 },
            { "2 Key", KeyboardKeyCodes.N2 },
            { "3 Key", KeyboardKeyCodes.N3 },
            { "4 Key", KeyboardKeyCodes.N4 },
            { "5 Key", KeyboardKeyCodes.N5 },
            { "6 Key", KeyboardKeyCodes.N6 },
            { "Q Key", KeyboardKeyCodes.Q },
            { "W Key", KeyboardKeyCodes.W },
            { "E Key", KeyboardKeyCodes.E },
            { "R Key", KeyboardKeyCodes.R },
            { "D Key", KeyboardKeyCodes.D },
            { "F Key", KeyboardKeyCodes.F }
        };

        private void Start()
        {
            leftClickDropdown.options = VRKeyCodeNames.Select(enumElement => new TMP_Dropdown.OptionData(enumElement.Key)).ToList();
            rightClickDropdown.options = VRKeyCodeNames.Select(enumElement => new TMP_Dropdown.OptionData(enumElement.Key)).ToList();
            mainHandDropdown.options = HandednessNames.Select(enumElement => new TMP_Dropdown.OptionData(enumElement.Key)).ToList();
            potionKeyDropdown.options = KeyboardKeyCodesNames.Select(enumElement => new TMP_Dropdown.OptionData(enumElement.Key)).ToList();

            championDropdown.options = Enum.GetNames(typeof(Champion)).Select(enumElement => new TMP_Dropdown.OptionData(enumElement.ToFirstUpper(), ChampionSpriteDownloader.GetIcon(enumElement))).ToList();

            LoadSettings();
        }

        public void LoadSettings()
        {
            leftClickDropdown.value = VRKeyCodeNames.IndexOf(ConfigManager.LeftClick);
            rightClickDropdown.value = VRKeyCodeNames.IndexOf(ConfigManager.RightClick);
            mainHandDropdown.value = HandednessNames.IndexOf(ConfigManager.MainHand);
            potionKeyDropdown.value = KeyboardKeyCodesNames.IndexOf(ConfigManager.PotionKey);

            championDropdown.SetSelectedByText(ConfigManager.Champion.ToFirstUpper());

            monitorSizeSlider.value = ConfigManager.MonitorSize;
        }

        public void SaveSettings()
        {
            ConfigManager.LeftClick = VRKeyCodeNames[leftClickDropdown.GetSelectedText()];
            ConfigManager.RightClick = VRKeyCodeNames[rightClickDropdown.GetSelectedText()];
            ConfigManager.MainHand = HandednessNames[mainHandDropdown.GetSelectedText()];
            ConfigManager.MonitorSize = (float)Math.Round((decimal)monitorSizeSlider.value, 2);
            ConfigManager.Champion = championDropdown.GetSelectedText().ToUpper();
            ConfigManager.PotionKey = KeyboardKeyCodesNames[potionKeyDropdown.GetSelectedText()];

            ConfigManager.Save();
        }
    }
}

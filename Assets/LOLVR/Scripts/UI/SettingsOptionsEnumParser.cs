using System;
using System.Linq;
using Edwon.VR;
using LOLVR.Enums;
using TMPro;
using UnityEngine;

namespace LOLVR.UI
{
    public class SettingsOptionsEnumParser : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown[] vrKeyCodes;
        [SerializeField] private TMP_Dropdown handedness;
        [SerializeField] private TMP_Dropdown champions;

        private void Awake()
        {
            foreach (TMP_Dropdown vrKeyCode in vrKeyCodes)
            {
                vrKeyCode.options = Enum.GetNames(typeof(VRKeyCodes)).Select(enumElement => new TMP_Dropdown.OptionData(enumElement)).ToList();
            }

            handedness.options = Enum.GetNames(typeof(Handedness)).Select(enumElement => new TMP_Dropdown.OptionData(enumElement)).ToList();

            champions.options = Enum.GetNames(typeof(Champion)).Select(enumElement => new TMP_Dropdown.OptionData(enumElement, ChampionSpriteDownloader.GetIcon(enumElement))).ToList();
        }

        public void ReloadOptions() => Awake();
    }
}

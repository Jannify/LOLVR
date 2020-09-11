using System;
using System.Linq;
using Edwon.VR;
using LOLVR.Assets.Scripts.UI;
using TMPro;
using UnityEngine;

namespace LOLVR.UI
{
    public class SettingsOptionsEnumParser : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private bool vrKeyCodes;
        [SerializeField] private bool handedness;
        [SerializeField] private bool champions;

        private void Awake()
        {
            if (vrKeyCodes)
            {
                dropdown.options = Enum.GetNames(typeof(VRKeyCodes)).Select(enumElement => new TMP_Dropdown.OptionData(enumElement)).ToList();
            }
            else if (handedness)
            {
                dropdown.options = Enum.GetNames(typeof(Handedness)).Select(enumElement => new TMP_Dropdown.OptionData(enumElement)).ToList();
            }
            else if (champions)
            {
                dropdown.options = Enum.GetNames(typeof(Champion)).Select(enumElement => new TMP_Dropdown.OptionData(enumElement, ChampionSpriteDownloader.GetIcon(enumElement))).ToList();
            }
        }

        public void ReloadOptions() => Awake();
    }
}

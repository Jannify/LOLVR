using Edwon.VR;
using Edwon.VR.Gesture;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LOLVR.UI
{
    public class VRSettingsUI : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown leftClickDropdown, rightClickDropdown, championDropdown, mainHandDropdown;
        [SerializeField] private Slider monitorSizeSlider;
        [SerializeField] private Transform uiPanel;
        [SerializeField] private VRGestureRig rig;

        private bool showUI;
        private Transform vrMenuHand, vrCam;
        private CanvasGroup uiPanelCanvasCanvasGroup;
        private const float OFFSET_Z = 0.2f;

        private void LoadSettings()
        {
            leftClickDropdown.value = leftClickDropdown.options.IndexOf(leftClickDropdown.options.First(option => option.text == ConfigManager.LeftClick.ToString()));
            rightClickDropdown.value = rightClickDropdown.options.IndexOf(rightClickDropdown.options.First(option => option.text == ConfigManager.RightClick.ToString()));
            mainHandDropdown.value = mainHandDropdown.options.IndexOf(mainHandDropdown.options.First(option => option.text == ConfigManager.MainHand.ToString()));
            championDropdown.value = championDropdown.options.IndexOf(championDropdown.options.First(option => option.text == ConfigManager.Champion));
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
            HandGestureRecognizer.ReloadNeuralNet();
        }

        private void Start()
        {
            LoadSettings();
            rig.CreateVRLaserPointer();
            vrMenuHand = rig.GetHand(rig.mainHand == Handedness.Left ? Handedness.Right : Handedness.Left); //opposite hand
            vrCam = rig.head;
            uiPanelCanvasCanvasGroup = uiPanel.GetComponent<CanvasGroup>();
            Utils.ToggleCanvasGroup(uiPanelCanvasCanvasGroup, showUI);
        }

        private void Update()
        {
            if (showUI)
            {
                Vector3 handToCamVector = vrCam.position - vrMenuHand.position;
                uiPanel.position = vrMenuHand.position + (OFFSET_Z * handToCamVector);
                if (-handToCamVector != Vector3.zero) uiPanel.rotation = Quaternion.LookRotation(-handToCamVector, Vector3.up);
            }

            if (Input.GetKeyDown((KeyCode)VRKeyCodes.MENU))
            {
                showUI = !showUI;
                Utils.ToggleCanvasGroup(uiPanelCanvasCanvasGroup, showUI);
            }
        }
    }
}
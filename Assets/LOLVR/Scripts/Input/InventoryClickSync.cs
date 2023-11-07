using System.Collections.Generic;
using LOLVR.Enums;
using UnityEngine;
using UnityEngine.XR;

namespace LOLVR.Input
{
    public class InventoryClickSync : MonoBehaviour
    {
        private const float SELECTION_BORDER = 0.65f;
        private const float SELECTION_THRESHOLD = 0.5f;

        private readonly List<InputDevice> devices = new List<InputDevice>();
        private Vector2 lastSelection;
        private bool wasPressed;

        private void OnEnable()
        {
            List<InputDevice> allDevices = new List<InputDevice>();
            InputDevices.GetDevices(allDevices);
            foreach (InputDevice device in allDevices)
            {
                InputDevices_deviceConnected(device);
            }

            InputDevices.deviceConnected += InputDevices_deviceConnected;
            InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
        }

        private void OnDisable()
        {
            InputDevices.deviceConnected -= InputDevices_deviceConnected;
            InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
            devices.Clear();
        }

        private void InputDevices_deviceConnected(InputDevice device)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right) &&
                (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _) ||
                device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out _)))
            {
                devices.Add(device);
            }
        }

        private void InputDevices_deviceDisconnected(InputDevice device)
        {
            if (devices.Contains(device))
            {
                devices.Remove(device);
            }
        }

        private void Update()
        {
            foreach (InputDevice device in devices)
            {
                device.TryGetFeatureValue(CommonUsages.primary2DAxis, out lastSelection);

                if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool clicked) && clicked)
                {
                    if (!wasPressed)
                    {
                        ButtonPressed();
                    }
                }
                else
                {
                    wasPressed = false;
                }
            }
        }


        private void ButtonPressed()
        {
            wasPressed = true;
            if (lastSelection.sqrMagnitude < SELECTION_THRESHOLD)
            {
                return;
            }

            if (lastSelection.y > 0)
            {
                if (lastSelection.x < -SELECTION_BORDER)
                {
                    SendAction.SimulateKey(KeyboardKeyCodes.N1);
                }
                else if (lastSelection.x < SELECTION_BORDER)
                {
                    SendAction.SimulateKey(KeyboardKeyCodes.N2);
                }
                else
                {
                    SendAction.SimulateKey(KeyboardKeyCodes.N3);
                }
            }
            else
            {
                if (lastSelection.x < -SELECTION_BORDER)
                {
                    SendAction.SimulateKey(KeyboardKeyCodes.N4);
                }
                else if (lastSelection.x < SELECTION_BORDER)
                {
                    SendAction.SimulateKey(KeyboardKeyCodes.N5);
                }
                else
                {
                    SendAction.SimulateKey(KeyboardKeyCodes.N6);
                }
            }


        }
    }
}

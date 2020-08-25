using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityRawInput;

namespace LOLVR
{
    public class TransmittingKeys : MonoBehaviour
    {
        private readonly List<InputDevice> controller = new List<InputDevice>();

        private readonly Dictionary<InputFeatureUsage<bool>, RawKey>[] buttonMappings = { new Dictionary<InputFeatureUsage<bool>, RawKey>(), new Dictionary<InputFeatureUsage<bool>, RawKey>() };
        private readonly Dictionary<InputFeatureUsage<bool>, bool>[] isPressed = { new Dictionary<InputFeatureUsage<bool>, bool>(), new Dictionary<InputFeatureUsage<bool>, bool>() };

        private void Start()
        {
            SetupXRDevices();

            buttonMappings[0].Add(CommonUsages.secondaryButton, RawKey.Q);
            buttonMappings[0].Add(CommonUsages.primaryButton, RawKey.W);
            buttonMappings[1].Add(CommonUsages.secondaryButton, RawKey.E);
            buttonMappings[1].Add(CommonUsages.primaryButton, RawKey.R);
            buttonMappings[1].Add(CommonUsages.gripButton, RawKey.RightButton);
        }

        private void Update()
        {
            if (controller.Count < 1) return;

            for (int i = 0; i < 2; i++)
            {
                foreach (KeyValuePair<InputFeatureUsage<bool>, RawKey> map in buttonMappings[i])
                {
                    if (controller[i].TryGetFeatureValue(map.Key, out bool triggerValue) && triggerValue)
                    {
                        if (!isPressed[i][map.Key])
                        {
                            isPressed[i][map.Key] = true;
                            SendingKeystrokes.SimulateKey(map.Value);
                        }
                    }
                    else
                    {
                        isPressed[i][map.Key] = false;
                    }
                }
            }
        }


        private void SetupXRDevices()
        {
            Debug.Log("Registering XR-devices");
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller, controller);
            foreach (InputDevice device in controller)
            {
                Debug.Log($"'{device.name}' found.");
            }
        }
    }
}
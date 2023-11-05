#if EDWON_VR_OPEN_XR

using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace Edwon.VR.Input
{
    public class VRControllerInputOpenXR : MonoBehaviour, IInput
    {
        private Handedness handedness;
        private VRInputDevice inputDevice;

        public VRControllerInputOpenXR Init(Handedness handy)
        {
            handedness = handy;

            foreach (InputDevice device in InputSystem.devices)
            {
                TrySetMainHandDevice(device);
            }

            InputSystem.onDeviceChange += (device, change) =>
            {
                switch (change)
                {
                    case InputDeviceChange.Added:
                        TrySetMainHandDevice(device);
                        break;
                    case InputDeviceChange.Removed:
                        if (inputDevice != null && device == inputDevice.Device)
                        {
                            inputDevice = null;
                        }

                        break;
                }
            };
            return this;

            void TrySetMainHandDevice(InputDevice device)
            {
                if (device.name.Contains("OpenXR", StringComparison.OrdinalIgnoreCase))
                {
                    if ((handedness == Handedness.Left && device.usages.Contains(CommonUsages.LeftHand)) ||
                        (handedness == Handedness.Right && device.usages.Contains(CommonUsages.RightHand)))
                    {
                        inputDevice = new VRInputDevice(device);
                    }
                }
            }
        }

        public bool GetButton(InputOptions.Button button)
        {
            if (inputDevice != null)
            {
                return button switch
                {
                    InputOptions.Button.Trigger1 => inputDevice.TriggerPress.IsPressed(),
                    InputOptions.Button.Trigger2 => inputDevice.GripPress.IsPressed(),
                    InputOptions.Button.Button1 => inputDevice.Primary.IsPressed(),
                    InputOptions.Button.Button2 => inputDevice.Secondary.IsPressed(),
                    InputOptions.Button.Thumbstick => inputDevice.ThumbstickPress.IsPressed(),
                    _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
                };
            }

            return false;
        }

        public bool GetButtonDown(InputOptions.Button button)
        {
            if (inputDevice != null)
            {
                return button switch
                {
                    InputOptions.Button.Trigger1 => inputDevice.TriggerPress.wasPressedThisFrame,
                    InputOptions.Button.Trigger2 => inputDevice.GripPress.wasPressedThisFrame,
                    InputOptions.Button.Button1 => inputDevice.Primary.wasPressedThisFrame,
                    InputOptions.Button.Button2 => inputDevice.Secondary.wasPressedThisFrame,
                    InputOptions.Button.Thumbstick => inputDevice.ThumbstickPress.wasPressedThisFrame,
                    _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
                };
            }

            return false;
        }

        public bool GetButtonUp(InputOptions.Button button)
        {
            if (inputDevice != null)
            {
                return button switch
                {
                    InputOptions.Button.Trigger1 => inputDevice.TriggerPress.wasReleasedThisFrame,
                    InputOptions.Button.Trigger2 => inputDevice.GripPress.wasReleasedThisFrame,
                    InputOptions.Button.Button1 => inputDevice.Primary.wasReleasedThisFrame,
                    InputOptions.Button.Button2 => inputDevice.Secondary.wasReleasedThisFrame,
                    InputOptions.Button.Thumbstick => inputDevice.ThumbstickPress.wasReleasedThisFrame,
                    _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
                };
            }

            return false;
        }

        public bool GetTouch(InputOptions.Touch touch)
        {
            if (inputDevice != null)
            {
                return touch switch
                {
                    InputOptions.Touch.Trigger1 => inputDevice.TriggerTouch.isPressed,
                    InputOptions.Touch.Trigger2 => inputDevice.GripTouch.isPressed,
                    _ => throw new ArgumentOutOfRangeException(nameof(touch), touch, null)
                };
            }

            return false;
        }

        public bool GetTouchDown(InputOptions.Touch touchDown)
        {
            if (inputDevice != null)
            {
                return touchDown switch
                {
                    InputOptions.Touch.Trigger1 => inputDevice.TriggerTouch.wasPressedThisFrame,
                    InputOptions.Touch.Trigger2 => inputDevice.GripTouch.wasPressedThisFrame,
                    _ => throw new ArgumentOutOfRangeException(nameof(touchDown), touchDown, null)
                };
            }

            return false;
        }

        public float GetAxis1D(InputOptions.Axis1D axis1D)
        {
            if (inputDevice != null)
            {
                return axis1D switch
                {
                    InputOptions.Axis1D.Trigger1 => inputDevice.Trigger.value,
                    InputOptions.Axis1D.Trigger2 => inputDevice.Grip.value,
                    _ => throw new ArgumentOutOfRangeException(nameof(axis1D), axis1D, null)
                };
            }

            return 0f;
        }

        public Vector2 GetAxis2D(InputOptions.Axis2D axis2D)
        {
            if (inputDevice != null)
            {
                return axis2D switch
                {
                    InputOptions.Axis2D.Thumbstick => inputDevice.Thumbstick.value,
                    _ => throw new ArgumentOutOfRangeException(nameof(axis2D), axis2D, null)
                };
            }

            return Vector2.zero;
        }

        public void InputUpdate()
        {
        }

        private class VRInputDevice
        {
            public readonly InputDevice Device;

            public readonly StickControl Thumbstick;
            public readonly ButtonControl ThumbstickPress;

            public readonly AxisControl Trigger;
            public readonly ButtonControl TriggerPress;
            public readonly ButtonControl TriggerTouch;

            public readonly AxisControl Grip;
            public readonly ButtonControl GripPress;
            public readonly ButtonControl GripTouch;

            public readonly ButtonControl Primary;
            public readonly ButtonControl Secondary;

            [SuppressMessage("ReSharper", "StringLiteralTypo")]
            public VRInputDevice(InputDevice device)
            {
                Device = device;

                foreach (InputControl inputControl in device.children)
                {
                    switch (inputControl)
                    {
                        case StickControl { displayName: "thumbstick" } thumbstickControl:
                            Thumbstick = thumbstickControl;
                            break;
                        case ButtonControl { displayName: "thumbstickclicked" } thumbstickPressedControl:
                            ThumbstickPress = thumbstickPressedControl;
                            break;
                        case AxisControl { displayName: "trigger" } triggerControl:
                            Trigger = triggerControl;
                            break;
                        case ButtonControl { displayName: "triggerpressed" } triggerPressControl:
                            TriggerPress = triggerPressControl;
                            break;
                        case ButtonControl { displayName: "triggertouched" } triggerTouchedControl:
                            TriggerTouch = triggerTouchedControl;
                            break;
                        case AxisControl { displayName: "grip" } gripControl:
                            Grip = gripControl;
                            break;
                        case ButtonControl { displayName: "grippressed" } gripTouchedControl:
                            GripTouch = GripPress = gripTouchedControl;
                            break;
                        case ButtonControl { displayName: "primarybutton" } primaryControl:
                            Primary = primaryControl;
                            break;
                        case ButtonControl { displayName: "secondarybutton" } secondaryControl:
                            Secondary = secondaryControl;
                            break;
                    }
                }
            }
        }
    }
}
#endif

using UnityEngine;

namespace LOLVR
{
    public enum VRKeyCodes
    {
        LEFT_PRIMARY = KeyCode.JoystickButton2,
        LEFT_SECONDARY = KeyCode.JoystickButton3,
        RIGHT_PRIMARY = KeyCode.JoystickButton0,
        RIGHT_SECONDARY = KeyCode.JoystickButton1,
        LEFT_AXIS_CLICK = KeyCode.JoystickButton8,
        LEFT_AXIS_TOUCH = KeyCode.JoystickButton16,
        RIGHT_AXIS_CLICK = KeyCode.JoystickButton9,
        RIGHT_AXIS_TOUCH = KeyCode.JoystickButton17,

        MENU = KeyCode.JoystickButton6
    }

    public static class VRAxis
    {
        public const string LEFT_TRIGGER = "XRI_Left_Trigger";
        public  const string RIGHT_TRIGGER = "XRI_Left_Trigger";
        public  const string LEFT_GRIP = "XRI_Left_Grip";
        public  const string RIGHT_GRIP = "XRI_Left_Grip";
        public  const string LEFT_THUMBSTICK_VERTICAL = "XRI_Left_Primary2DAxis_Vertical";
        public  const string LEFT_THUMBSTICK_HORIZONTAL = "XRI_Left_Primary2DAxis_Horizontal";
        public  const string RIGHT_THUMBSTICK_VERTICAL = "XRI_Right_Primary2DAxis_Vertical";
        public  const string RIGHT_THUMBSTICK_HORIZONTAL = "XRI_Right_Primary2DAxis_Horizontal";
    }
}
using UnityEngine;

namespace LOLVR.Enums
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

    public enum KeyboardKeyCodes : ushort
    {
        N1 = 0x02,
        N2 = 0x03,
        N3 = 0x04,
        N4 = 0x05,
        N5 = 0x06,
        N6 = 0x07,
        Q = 0x10,
        W = 0x11,
        E = 0x12,
        R = 0x13,
        D = 0x20,
        F = 0x21
    }

    //[SuppressMessage("ReSharper", "InconsistentNaming")]
    //public enum VRAxisCodes
    //{
    //    XRI_Left_Trigger,
    //    XRI_Right_Trigger,
    //    XRI_Left_Grip,
    //    XRI_Right_Grip,
    //    XRI_Left_Primary2DAxis_Vertical,
    //    XRI_Left_Primary2DAxis_Horizontal,
    //    XRI_Right_Primary2DAxis_Vertical,
    //    XRI_Right_Primary2DAxis_Horizontal = OVRInput.Axis2D.SecondaryThumbstick
    //}
}
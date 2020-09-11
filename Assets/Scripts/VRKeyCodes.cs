using System.Diagnostics.CodeAnalysis;
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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum VRAxisCodes
    {
        XRI_Left_Trigger,
        XRI_Right_Trigger,
        XRI_Left_Grip,
        XRI_Right_Grip,
        XRI_Left_Primary2DAxis_Vertical,
        XRI_Left_Primary2DAxis_Horizontal,
        XRI_Right_Primary2DAxis_Vertical,
        XRI_Right_Primary2DAxis_Horizontal
    }
}
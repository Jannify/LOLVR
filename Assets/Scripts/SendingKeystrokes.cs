using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityRawInput;

namespace LOLVR
{
    public static class SendingKeystrokes
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public static void SimulateKey(RawKey key)
        {
            SendInput(2, new[] { SimulateKeyboardDown(key), SimulateKeyboardUp(key) }, Marshal.SizeOf(new INPUT()));
        }

        public static void SimulateKeys(RawKey[] keys)
        {
            List<INPUT> inputs = keys.Select(SimulateKeyboardDown).ToList();
            inputs.AddRange(keys.Select(SimulateKeyboardUp));

            SendInput((uint)keys.Length * 2, inputs.ToArray(), Marshal.SizeOf(new INPUT()));
        }

        public static void SimulateMouseLeftClick()
        {
            SendInput(2, new[] { SimulateMouseLeftDown(), SimulateMouseLeftUp() }, Marshal.SizeOf(new INPUT()));
        }

        public static void SimulateMouseRightClick()
        {
            SendInput(2, new[] { SimulateMouseRightDown(), SimulateMouseRightUp() }, Marshal.SizeOf(new INPUT()));
        }

        private static INPUT SimulateKeyboardDown(RawKey key)
        {
            INPUT keyDownInput = new INPUT { type = SendInputEventType.InputKeyboard };
            keyDownInput.mkhi.ki.wVk = key;
            return keyDownInput;
        }


        private static INPUT SimulateKeyboardUp(RawKey key)
        {
            INPUT keyUpInput = new INPUT { type = SendInputEventType.InputKeyboard };
            keyUpInput.mkhi.ki.wVk = key;
            keyUpInput.mkhi.ki.dwFlags = 2;
            return keyUpInput;
        }

        private static INPUT SimulateMouseLeftDown()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            return mouseDownInput;
        }

        private static INPUT SimulateMouseLeftUp()
        {
            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            return mouseUpInput;
        }

        private static INPUT SimulateMouseRightDown()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
            return mouseDownInput;

        }

        private static INPUT SimulateMouseRightUp()
        {
            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
            return mouseUpInput;
        }
    }
}
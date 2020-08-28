using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LOLVR.InputStructs;

namespace LOLVR
{
    public static class SendAction
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT pInputs, int cbSize);
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        public static void SimulateKey(ushort key)
        {
            SendInput(1, KeyboardDown(key), Marshal.SizeOf(new INPUT()));
            SendInput(1, KeyboardUp(key), Marshal.SizeOf(new INPUT()));
        }
        public static void SimulateKeys(ushort[] keys)
        {
            List<INPUT> inputs = keys.Select(KeyboardDown).ToList();
            inputs.AddRange(keys.Select(KeyboardUp));

            SendInput((uint)keys.Length * 2, inputs.ToArray(), Marshal.SizeOf(new INPUT()));
        }

        public static void SimulateMouseLeftClick() => SendInput(2, new[] { MouseLeftDown(), MouseLeftUp() }, Marshal.SizeOf(new INPUT()));
        public static void SimulateMouseRightClick() => SendInput(2, new[] { MouseRightDown(), MouseRightUp() }, Marshal.SizeOf(new INPUT()));

        public static void SimulateMouseLeftUp() => SendInput(1, MouseLeftUp(), Marshal.SizeOf(new INPUT()));
        public static void SimulateMouseRightUp() => SendInput(1, MouseRightUp(), Marshal.SizeOf(new INPUT()));
        public static void SimulateMouseLeftDown() => SendInput(1, MouseLeftDown(), Marshal.SizeOf(new INPUT()));
        public static void SimulateMouseRightDown() => SendInput(1, MouseRightDown(), Marshal.SizeOf(new INPUT()));

        private static INPUT KeyboardDown(ushort key)
        {
            INPUT input = new INPUT { type = SendInputEventType.InputKeyboard };
            input.mkhi.ki.wScan = key;
            input.mkhi.ki.dwFlags = KeyboardEventFlags.KeyDown | KeyboardEventFlags.Scancode;
            input.mkhi.ki.dwExtraInfo = IntPtr.Zero;
            return input;
        }


        private static INPUT KeyboardUp(ushort key)
        {
            INPUT input = new INPUT { type = SendInputEventType.InputKeyboard };
            input.mkhi.ki.wScan = key;
            input.mkhi.ki.dwFlags = KeyboardEventFlags.KeyUp | KeyboardEventFlags.Scancode;
            input.mkhi.ki.dwExtraInfo = IntPtr.Zero;
            return input;
        }

        private static INPUT MouseLeftDown()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            return mouseDownInput;
        }

        private static INPUT MouseLeftUp()
        {
            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            return mouseUpInput;
        }

        private static INPUT MouseRightDown()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
            return mouseDownInput;

        }

        private static INPUT MouseRightUp()
        {
            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
            return mouseUpInput;
        }
    }
}
using System;
using System.Runtime.InteropServices;
using System.Threading;
using LOLVR.Enums;
using LOLVR.InputStructs;

namespace LOLVR.Input
{
    public static class SendAction
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT pInputs, int cbSize);

        private static readonly Random random = new Random();
        private const int MIN_RANDOM = 9, MAX_RANDOM = 23;

        public static void SimulateKey(KeyboardKeyCodes key)
        {
            SendInput(1, KeyboardDown(key), Marshal.SizeOf(new INPUT()));
            Thread.Sleep(random.Next(MIN_RANDOM, MAX_RANDOM));
            SendInput(1, KeyboardUp(key), Marshal.SizeOf(new INPUT()));
        }

        public static void SimulateMouseLeftUp() => SendInput(1, MouseLeftUp(), Marshal.SizeOf(new INPUT()));
        public static void SimulateMouseRightUp() => SendInput(1, MouseRightUp(), Marshal.SizeOf(new INPUT()));
        public static void SimulateMouseLeftDown() => SendInput(1, MouseLeftDown(), Marshal.SizeOf(new INPUT()));
        public static void SimulateMouseRightDown() => SendInput(1, MouseRightDown(), Marshal.SizeOf(new INPUT()));

        private static INPUT KeyboardDown(KeyboardKeyCodes key)
        {
            INPUT input = new INPUT { type = SendInputEventType.InputKeyboard };
            input.mkhi.ki.wScan = (ushort) key;
            input.mkhi.ki.dwFlags = KeyboardEventFlags.KeyDown| KeyboardEventFlags.Scancode;
            input.mkhi.ki.dwExtraInfo = IntPtr.Zero;
            return input;
        }


        private static INPUT KeyboardUp(KeyboardKeyCodes key)
        {
            INPUT input = new INPUT { type = SendInputEventType.InputKeyboard };
            input.mkhi.ki.wScan = (ushort) key;
            input.mkhi.ki.dwFlags = KeyboardEventFlags.KeyUp | KeyboardEventFlags.Scancode;
            input.mkhi.ki.dwExtraInfo = IntPtr.Zero;
            return input;
        }

        private static INPUT MouseLeftDown()
        {
            INPUT mouseDownInput = new INPUT { type = SendInputEventType.InputMouse };
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            return mouseDownInput;
        }

        private static INPUT MouseLeftUp()
        {
            INPUT mouseUpInput = new INPUT { type = SendInputEventType.InputMouse };
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            return mouseUpInput;
        }

        private static INPUT MouseRightDown()
        {
            INPUT mouseDownInput = new INPUT { type = SendInputEventType.InputMouse };
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
            return mouseDownInput;

        }

        private static INPUT MouseRightUp()
        {
            INPUT mouseUpInput = new INPUT { type = SendInputEventType.InputMouse };
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
            return mouseUpInput;
        }
    }
}

using gamepad_mouse_controller.Controller;
using System;
using System.Runtime.InteropServices;

namespace gamepad_mouse_controller.Model
{
    class GamepadConfiguration : IInput
    {
        #region Dlls
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern void mouse_event(uint flag, uint _x, uint _y, int btn, uint exInfo);
        private const int MOUSEEVENT_LEFTDOWN = 0x02;
        private const int MOUSEEVENT_LEFTUP = 0x04;
        private const int MOUSEEVENT_RIGHTDOWN = 0x08;
        private const int MOUSEEVENT_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_WHEEL = 0x0800;

        [DllImport("user32.dll")]
        public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);
        private const int VK_BROWSER_BACK = 0xA6;
        private const int VK_BROWSER_FORWARD = 0xA7;
        private const int VK_LWIN = 0x5B;
        private const int VK_LEFT = 0x25;
        private const int VK_UP = 0x26;
        private const int VK_RIGHT = 0x27;
        private const int VK_DOWN = 0x28;

        #endregion

        public void LAxis(int axis)
        {
            throw new System.NotImplementedException();
        }

        public void OnDownDown()
        {
            throw new System.NotImplementedException();
        }

        public void OnDownUp()
        {
            throw new System.NotImplementedException();
        }

        public void OnL1Down()
        {
            keybd_event(VK_BROWSER_FORWARD, 0, 0, 0);
        }

        public void OnL1Up()
        {
            keybd_event(VK_BROWSER_FORWARD, 0, 0x0002, 0);
        }

        public void OnL3Down()
        {
            throw new System.NotImplementedException();
        }

        public void OnL3Up()
        {
            throw new System.NotImplementedException();
        }

        public void OnLeftDown()
        {
            throw new System.NotImplementedException();
        }

        public void OnLeftUp()
        {
            throw new System.NotImplementedException();
        }

        public void OnODown()
        {
            mouse_event(MOUSEEVENT_RIGHTDOWN, 0, 0, 0, 0);
        }

        public void OnOUp()
        {
            mouse_event(MOUSEEVENT_RIGHTUP, 0, 0, 0, 0);
        }

        public void OnR1Down()
        {
            keybd_event(VK_BROWSER_BACK, 0, 0, 0);
        }

        public void OnR1Up()
        {
            keybd_event(VK_BROWSER_BACK, 0, 0x0002, 0);
        }

        public void OnR3Down()
        {
            throw new System.NotImplementedException();
        }

        public void OnR3Up()
        {
            throw new System.NotImplementedException();
        }

        public void OnRightDown()
        {
            throw new System.NotImplementedException();
        }

        public void OnRightUp()
        {
            throw new System.NotImplementedException();
        }

        public void OnSDown()
        {
            throw new System.NotImplementedException();
        }

        public void OnSelectDown()
        {
            throw new System.NotImplementedException();
        }

        public void OnSelectUp()
        {
            throw new System.NotImplementedException();
        }

        public void OnStartDown()
        {
            keybd_event(VK_LWIN, 0, 0, 0);
        }

        public void OnStartUp()
        {
            keybd_event(VK_LWIN, 0, 0x0002, 0);
        }

        public void OnSUp()
        {
            throw new System.NotImplementedException();
        }

        public void OnTDown()
        {
            throw new System.NotImplementedException();
        }

        public void OnTUp()
        {
            throw new System.NotImplementedException();
        }

        public void OnUpDown()
        {
            throw new System.NotImplementedException();
        }

        public void OnUpUp()
        {
            throw new System.NotImplementedException();
        }

        public void OnXDown()
        {
            mouse_event(MOUSEEVENT_LEFTDOWN, 0, 0, 0, 0);
        }

        public void OnXUp()
        {
            mouse_event(MOUSEEVENT_LEFTUP, 0, 0, 0, 0);
        }

        public void R2L2Axis(int axis)
        {
            throw new System.NotImplementedException();
        }

        public void RAxis(int axis)
        {
            throw new System.NotImplementedException();
        }
    }
}

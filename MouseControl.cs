using System;
using System.Collections.Generic;
using System.Timers;
using System.ComponentModel;
using SlimDX.DirectInput;
using System.Runtime.InteropServices;

namespace gamepad_mouse_controller
{
    class MouseControl
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

        #endregion

        //Background Worker stuff
        private readonly BackgroundWorker worker;
        private Timer timer = new Timer();

        //Joystick stuff
        public Joystick joystick;

        //Variables
        private int scrollSpeed = 1;
        private bool[] previousButtonState;


        public MouseControl(Joystick joystick)
        {
            this.joystick = joystick;
            previousButtonState = this.joystick.GetCurrentState().GetButtons();

            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;

            timer.Interval = 10;
            timer.Enabled = true;

            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            timer.Elapsed += new ElapsedEventHandler(mouseMove);
        }

        private void mouseMove(object sender, ElapsedEventArgs e)
        {
            JoystickState state = joystick.GetCurrentState();

            int y = state.Y * state.Y * state.Y / 100;
            int x = state.X * state.X * state.X / 100;

            int w = -state.RotationY * scrollSpeed;
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, w, 0);

            bool[] buttons = state.GetButtons();

            Win32Point mousePos = new Win32Point();
            GetCursorPos(ref mousePos);
            SetCursorPos(mousePos.X + x, mousePos.Y + y);

            //X pressed
            if (buttons[0] && !previousButtonState[0])
            {
                mouse_event(MOUSEEVENT_LEFTDOWN, 0, 0, 0, 0);
            }
            //X released
            else if (!buttons[0] && previousButtonState[0])
            {
                mouse_event(MOUSEEVENT_LEFTUP, 0, 0, 0, 0);
            }

            //O pressed
            if (buttons[1] && !previousButtonState[1])
            {
                mouse_event(MOUSEEVENT_RIGHTDOWN, 0, 0, 0, 0);
            }
            //O released
            else if (!buttons[1] && previousButtonState[1])
            {
                mouse_event(MOUSEEVENT_RIGHTUP, 0, 0, 0, 0);
            }

            //L3 pressed
            if(buttons[9])
            {
                scrollSpeed = 3;
            }
            else if (w == 0)
            {
                scrollSpeed = 1;
            }

            //R1 pressed
            if(buttons[4] && !previousButtonState[4])
            {
                keybd_event(VK_BROWSER_BACK, 0, 0, 0);
            }
            //R1 released
            else if(!buttons[4] && previousButtonState[4])
            {
                keybd_event(VK_BROWSER_BACK, 0, 0x0002, 0);
            }
            
            //L1 pressed
            if(buttons[5] && !previousButtonState[5])
            {
                keybd_event(VK_BROWSER_FORWARD, 0, 0, 0);
            }
            //L1 released
            else if(!buttons[5] && previousButtonState[5])
            {
                keybd_event(VK_BROWSER_FORWARD, 0, 0x0002, 0);
            }

            //Start pressed
            if(buttons[7] && !previousButtonState[7])
            {
                keybd_event(VK_LWIN, 0, 0, 0);
            }
            else if(!buttons[7] && previousButtonState[7])
            {
                keybd_event(VK_LWIN, 0, 0x0002, 0);
            }

            previousButtonState = buttons;
        }
    }
}

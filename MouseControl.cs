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


        private readonly BackgroundWorker worker;
        private Timer timer= new Timer();

        private DirectInput input = new DirectInput();
        private Joystick[] joysticks;
        public Joystick joystick;

        private bool leftDown = false;
        private bool rightDown = false;

        public MouseControl(Joystick joystick)
        {
            this.joystick = joystick;

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

            int w = -state.RotationY;
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, w, 0);

            bool[] buttons = state.GetButtons();

            Win32Point mousePos = new Win32Point();
            GetCursorPos(ref mousePos);
            SetCursorPos(mousePos.X + x, mousePos.Y + y);

            if (buttons[0])
            {
                if (!leftDown)
                {
                    leftDown = true;
                    mouse_event(MOUSEEVENT_LEFTDOWN, 0, 0, 0, 0);
                }
            }
            else
            {
                if (leftDown)
                {
                    leftDown = false;
                    mouse_event(MOUSEEVENT_LEFTUP, 0, 0, 0, 0);
                }
            }

            if (buttons[1])
            {
                if (!rightDown)
                {
                    rightDown = true;
                    mouse_event(MOUSEEVENT_RIGHTDOWN, 0, 0, 0, 0);
                }
            }
            else
            {
                if (rightDown)
                {
                    rightDown = false;
                    mouse_event(MOUSEEVENT_RIGHTUP, 0, 0, 0, 0);
                }
            }
        }

        private Joystick[] GetJoysticks()
        {
            List<Joystick> sticks = new List<Joystick>();
            foreach (DeviceInstance device in input.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                try
                {
                    var stick = new Joystick(input, device.InstanceGuid);
                    stick.Acquire();
                    foreach (DeviceObjectInstance deviceObject in stick.GetObjects())
                    {
                        if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                        {
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-10, 10);
                        }
                    }
                    sticks.Add(stick);
                }
                catch (DirectInputException)
                {

                }
            }
            return sticks.ToArray();
        }
    }
}

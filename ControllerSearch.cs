using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SlimDX.DirectInput;
using System.Timers;

namespace gamepad_mouse_controller
{
    class ControllerSearch
    {
        private readonly BackgroundWorker worker;
        private Timer timer = new Timer();

        private DirectInput input = new DirectInput();
        private IList<DeviceInstance> oldDeviceInstances;
        private List<MouseControl> joysticks;

        public ControllerSearch()
        {
            oldDeviceInstances = new List<DeviceInstance>();
            joysticks = new List<MouseControl>();

            timer.Interval = 1000;
            timer.Enabled = true;

            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            timer.Elapsed += new ElapsedEventHandler(GetJoysticks);
        }

        private void GetJoysticks(object sender, ElapsedEventArgs e)
        {
            IList<DeviceInstance> newDeviceInstances = input.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly);
            if (newDeviceInstances.Count > oldDeviceInstances.Count)
            {
                List<Joystick> sticks = new List<Joystick>();
                foreach (DeviceInstance device in input.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
                {
                    try
                    {
                        var stick = new Joystick(input, device.InstanceGuid);
                        stick.Acquire();

                        if (!checkIfNew(stick))
                        {
                            foreach (DeviceObjectInstance deviceObject in stick.GetObjects())
                            {
                                if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                                {
                                    stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-10, 10);
                                    stick.GetObjectPropertiesById((int)deviceObject.ObjectType).DeadZone = 500;
                                }
                            }
                            joysticks.Add(new MouseControl(stick));
                        }
                    }
                    catch (DirectInputException)
                    {

                    }
                }
            }
            oldDeviceInstances = newDeviceInstances;
        }

        private bool checkIfNew(Joystick stick)
        {
            foreach (MouseControl j in joysticks)
            {
                if(j.joystick.Properties.ProductId == stick.Properties.ProductId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

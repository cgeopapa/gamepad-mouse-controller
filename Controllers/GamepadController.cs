using gamepad_mouse_controller.Model;
using SlimDX.DirectInput;
using System.Collections.Generic;

namespace gamepad_mouse_controller.Controller
{
    class GamepadController
    {
        public static Gamepad[] Gamepads { get; private set; }

        public GamepadController()
        {
            Gamepads = GetDevices();
        }

        public Gamepad[] GetDevices()
        {
            int i = 1;

            DirectInput input = new DirectInput();
            IList<DeviceInstance> deviceInstances = input.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly);
            List<Gamepad> joysticks = new List<Gamepad>();
            foreach (DeviceInstance device in deviceInstances)
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
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).DeadZone = 500;
                        }
                    }
                    joysticks.Add(new Gamepad(stick, i++));
                }
                catch (DirectInputException)
                {}
            }
            return joysticks.ToArray();
        }
    }
}

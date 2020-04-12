using gamepad_mouse_controller.Model;

using SlimDX.DirectInput;
using System.Collections.Generic;

namespace gamepad_mouse_controller.Controllers
{
    class GamepadController
    {
        public Model.Gamepad[] Gamepads { get; }

        public GamepadController()
        {
            Gamepads = GetDevices();
        }

        public Model.Gamepad[] GetDevices()
        {
            DirectInput input = new DirectInput();
            IList<DeviceInstance> deviceInstances = input.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly);
            List<Model.Gamepad> joysticks = new List<Model.Gamepad>();
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
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).DeadZone = 100;
                        }
                    }
                    joysticks.Add(new Model.Gamepad(stick));
                }
                catch (DirectInputException)
                { }
            }
            return joysticks.ToArray();
        }
    }
}

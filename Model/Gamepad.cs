using SlimDX.DirectInput;
using System.Threading;

namespace gamepad_mouse_controller.Model
{
    public class Gamepad
    {
        private readonly GamepadConfiguration configuration;
        private readonly GamepadSettingsWindow window;
        private readonly ActionArgs args;
        private readonly Joystick device;

        private bool[] previousButtonState;
        private readonly Timer timer;

        public int index;
        public float mouseSensitivity = 1;
        public int scrollSensitivity = 1;

        public Gamepad(Joystick device, int index)
        {
            this.index = index;
            this.device = device;
            previousButtonState = device.GetCurrentState().GetButtons();

            configuration = new GamepadConfiguration("gamepad01", 10);
            window = new GamepadSettingsWindow(this);
            args = new ActionArgs(this);

            timer = new Timer(ManageInput, new AutoResetEvent(false), 0, 20);
        }

        private void ManageInput(object e)
        {
            JoystickState state = device.GetCurrentState();
            bool[] curButtonState = state.GetButtons();

            args.x = (int)(state.X * mouseSensitivity);
            args.y = (int)(state.Y * mouseSensitivity);
            configuration.action[10].Execute(args);

            args.x = state.RotationX / 10 * scrollSensitivity;
            args.y = -state.RotationY / 10 * scrollSensitivity;
            configuration.action[11].Execute(args);

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if (curButtonState[i] && !previousButtonState[i])
                    {
                        args.up = true;
                        configuration.action[i].Execute(args);
                    }
                    else if (!curButtonState[i] && previousButtonState[i])
                    {
                        args.up = false;
                        configuration.action[i].Execute(args);
                    }
                }
                catch (System.NotImplementedException) { }
            }
            previousButtonState = state.GetButtons();
        }

        public void ShowWindow()
        {
            window.ShowWindow();
        }
    }
}

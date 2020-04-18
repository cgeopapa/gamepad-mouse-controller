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
        private int previousArrows;
        private readonly Timer timer;

        public int index;
        public float mouseSensitivity = 1;
        public float scrollSensitivity = .1f;

        public Gamepad(Joystick device, int index)
        {
            this.index = index;
            this.device = device;
            previousButtonState = device.GetCurrentState().GetButtons();
            previousArrows = -1;

            configuration = new GamepadConfiguration("gamepad01", 10);
            window = new GamepadSettingsWindow(this);
            args = new ActionArgs(this);

            timer = new Timer(ManageInput, new AutoResetEvent(false), 0, 20);
        }

        private void ManageInput(object e)
        {
            JoystickState state = device.GetCurrentState();
            bool[] curButtonState = state.GetButtons();
            int arrows = state.GetPointOfViewControllers()[0];

            args.x = (int)(state.X * mouseSensitivity);
            args.y = (int)(state.Y * mouseSensitivity);
            configuration.action[14].Execute(args);

            args.x = (int)(state.RotationX * scrollSensitivity);
            args.y = (int)(-state.RotationY * scrollSensitivity);
            configuration.action[15].Execute(args);

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if (curButtonState[i] && !previousButtonState[i])
                    {
                        args.down = true;
                        configuration.action[i].Execute(args);
                    }
                    else if (!curButtonState[i] && previousButtonState[i])
                    {
                        args.down = false;
                        configuration.action[i].Execute(args);
                    }
                }
                catch (System.Exception) { }
            }
            for(int i = 0; i <= 27000; i += 9000)
            {
                if(arrows == i && previousArrows != i)
                {
                    args.down = true;
                    configuration.action[10 + i / 9000].Execute(args);
                }
                else if(arrows != i && previousArrows == i)
                {
                    args.down = false;
                    configuration.action[10 + i / 9000].Execute(args);
                }
            }
            previousArrows = arrows;
            previousButtonState = state.GetButtons();
        }

        public void ShowWindow()
        {
            window.ShowWindow();
        }
    }
}

using gamepad_mouse_controller.Model.Buttons;
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
        public bool IsActive {get; set;}
        private readonly System.Threading.Thread timer;

        public int Index { get; set; }
        public string Display
        {
            get
            {
                return "Gamepad " + Index;
            }
        }
        public float mouseSensitivity = 1;
        public float scrollSensitivity = .1f;

        public Gamepad(Joystick device, int index)
        {
            this.Index = index;
            IsActive = true;
            this.device = device;
            previousButtonState = device.GetCurrentState().GetButtons();
            previousArrows = -1;

            configuration = new GamepadConfiguration("gamepad01", 10);
            window = new GamepadSettingsWindow(this);
            args = new ActionArgs(this);

            //timer = new System.Threading.Timer(ManageInput, new AutoResetEvent(false), 0, 20);
            timer = new Thread(new ThreadStart(ManageInput));
            timer.IsBackground = true;
            timer.Start();
        }

        public void ChangeState()
        {
            IsActive = !IsActive;
        }

        private void ManageInput()
        {
            while (true)
            {
                JoystickState state = device.GetCurrentState();
                bool[] curButtonState = state.GetButtons();
                int arrows = state.GetPointOfViewControllers()[0];

                if (IsActive)
                {
                    args.x = (int)(state.X * mouseSensitivity);
                    args.y = (int)(state.Y * mouseSensitivity);
                    configuration.action[(int)GamepadButtons.LAxis].Execute(args);

                    args.x = (int)(state.RotationX * scrollSensitivity);
                    args.y = (int)(-state.RotationY * scrollSensitivity);
                    configuration.action[(int)GamepadButtons.RAxis].Execute(args);

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
                        catch (System.NullReferenceException) { }
                    }
                    for (int i = 0; i <= 27000; i += 9000)
                    {
                        if (arrows == i && previousArrows != i)
                        {
                            args.down = true;
                            configuration.action[10 + i / 9000].Execute(args);
                        }
                        else if (arrows != i && previousArrows == i)
                        {
                            args.down = false;
                            configuration.action[10 + i / 9000].Execute(args);
                        }
                    }
                }
                if (curButtonState[(int)GamepadButtons.L3] && !previousButtonState[(int)GamepadButtons.L3] && curButtonState[(int)GamepadButtons.R3] || curButtonState[(int)GamepadButtons.R3] && !previousButtonState[(int)GamepadButtons.R3] && curButtonState[(int)GamepadButtons.L3])
                {
                    args.down = true;
                    configuration.action[(int)GamepadButtons.L3_R3].Execute(args);
                }

                previousArrows = arrows;
                previousButtonState = curButtonState;

                Thread.Sleep(20);
            }
        }

        public void ShowWindow()
        {
            window.ShowWindow();
        }
    }
}

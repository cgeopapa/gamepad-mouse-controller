using OpenTK.Input;
using System;
using System.Threading;

namespace gamepad_mouse_controller.Model
{
    public class Gamepad
    {
        private readonly GamepadConfiguration configuration;
        private readonly GamepadSettingsWindow window;
        private readonly ActionArgs args;
        private readonly Timer timer;

        private GamePadState previousState;
        private int previousArrows;
        public bool IsActive {get; set;}

        public int index;
        public string Display
        {
            get
            {
                return "Gamepad " + index;
            }
        }

        public int mouseSensitivity = 600;
        public float scrollSensitivity = 1.1f;
        public int refreshRate = 60;

        public Gamepad(int index)
        {
            this.index = index;
            IsActive = true;
            previousState = GamePad.GetState(index);
            previousArrows = -1;

            configuration = new GamepadConfiguration("gamepad01", 10);
            window = new GamepadSettingsWindow(this);
            args = new ActionArgs(this);

            timer = new Timer(ManageInput, new AutoResetEvent(false), 0, 1000 / refreshRate);
        }

        public void ChangeState()
        {
            IsActive = !IsActive;
        }

        private void ManageInput(Object sender)
        {
            GamePadState state = GamePad.GetState(index);

            if (IsActive)
            {
                int s = mouseSensitivity / refreshRate;
                var lStick = state.ThumbSticks.Left;
                args.x = (int)(lStick.X * s);
                args.y = (int)(lStick.Y * (-s));
                configuration.config[Buttons.LAxis].Execute(args);

                var rStick = state.ThumbSticks.Right;
                args.x = (int)(rStick.X * scrollSensitivity);
                args.y = (int)(rStick.Y * scrollSensitivity);
                configuration.config[Buttons.RAxis].Execute(args);

                var buts = state.Buttons;
                var prevbuts = previousState.Buttons;
                if (buts.GetHashCode() != prevbuts.GetHashCode())
                {
                    if (prevbuts.A == ButtonState.Released && buts.A == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.A].Execute(args);
                    }
                    else if (prevbuts.A == ButtonState.Pressed && buts.A == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.A].Execute(args);
                    }

                    if (prevbuts.B == ButtonState.Released && buts.B == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.B].Execute(args);
                    }
                    else if (prevbuts.B == ButtonState.Pressed && buts.B == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.B].Execute(args);
                    }

                    if (prevbuts.X == ButtonState.Released && buts.X == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.X].Execute(args);
                    }
                    else if (prevbuts.X == ButtonState.Pressed && buts.X == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.X].Execute(args);
                    }

                    if (prevbuts.Y == ButtonState.Released && buts.Y == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.Y].Execute(args);
                    }
                    else if (prevbuts.Y == ButtonState.Pressed && buts.Y == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.Y].Execute(args);
                    }

                    if (prevbuts.Back == ButtonState.Released && buts.Back == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.Back].Execute(args);
                    }
                    else if (prevbuts.Back == ButtonState.Pressed && buts.Back == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.Back].Execute(args);
                    }

                    if (prevbuts.Start == ButtonState.Released && buts.Start == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.Start].Execute(args);
                    }
                    else if (prevbuts.Start == ButtonState.Pressed && buts.Start == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.Start].Execute(args);
                    }

                    if (prevbuts.LeftStick == ButtonState.Released && buts.LeftStick == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.LeftStick].Execute(args);
                    }
                    else if (prevbuts.LeftStick == ButtonState.Pressed && buts.LeftStick == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.LeftStick].Execute(args);
                    }

                    if (prevbuts.RightStick == ButtonState.Released && buts.RightStick == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.RightStick].Execute(args);
                    }
                    else if (prevbuts.RightStick == ButtonState.Pressed && buts.RightStick == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.RightStick].Execute(args);
                    }

                    if (prevbuts.LeftShoulder == ButtonState.Released && buts.LeftShoulder == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.LeftShoulder].Execute(args);
                    }
                    else if (prevbuts.LeftShoulder == ButtonState.Pressed && buts.LeftShoulder == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.LeftShoulder].Execute(args);
                    }

                    if (prevbuts.RightShoulder == ButtonState.Released && buts.RightShoulder == ButtonState.Pressed)
                    {
                        args.down = true;
                        configuration.config[Buttons.RightShoulder].Execute(args);
                    }
                    else if (prevbuts.RightShoulder == ButtonState.Pressed && buts.RightShoulder == ButtonState.Released)
                    {
                        args.down = false;
                        configuration.config[Buttons.RightShoulder].Execute(args);
                    }
                }
            }

            previousState = state;
        }

        public void ShowWindow()
        {
            window.ShowWindow();
        }
    }
}

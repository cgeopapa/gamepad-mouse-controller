using SlimDX.DirectInput;
using System;
using System.ComponentModel;
using System.Threading;

namespace gamepad_mouse_controller.Model
{
    class Gamepad
    {
        private GamepadConfiguration configuration;

        public readonly Joystick device;
        private bool[] previousButtonState;

        private readonly BackgroundWorker worker;
        private Timer timer;

        public Gamepad(Joystick device)
        {
            this.device = device;
            previousButtonState = device.GetCurrentState().GetButtons();
            configuration = new GamepadConfiguration("gamepad01", 10);

            timer = new Timer(ManageInput, new AutoResetEvent(false), 0, 30);

            //worker = new BackgroundWorker();
            //timer.Elapsed += new ElapsedEventHandler(ManageInput);

            //timer.Interval = 10;
            //timer.Enabled = true;

            //worker.RunWorkerAsync();
        }

        private void ManageInput(object sender)
        {
            JoystickState state = device.GetCurrentState();
            bool[] curButtonState = state.GetButtons();

            int y = state.Y;
            int x = state.X;
            configuration.action[10].Execute(x, y);

            y = -state.RotationY / 10;
            x = state.RotationX / 10;
            configuration.action[11].Execute(x, y);

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if (curButtonState[i] && !previousButtonState[i])
                    {
                        configuration.action[i].Execute(true);
                    }
                    else if (!curButtonState[i] && previousButtonState[i])
                    {
                        configuration.action[i].Execute(false);
                    }
                }
                catch (Exception) { }
            }
            previousButtonState = state.GetButtons();
        }
    }
}

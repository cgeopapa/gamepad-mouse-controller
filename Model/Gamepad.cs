using SlimDX.DirectInput;
using System;
using System.ComponentModel;
using System.Timers;

namespace gamepad_mouse_controller.Model
{
    class Gamepad
    {
        private GamepadConfiguration configuration;
        public readonly Joystick device;
        private bool[] previousButtonState;

        private readonly BackgroundWorker worker;
        private Timer timer = new Timer();

        private Action[] actions;

        public Gamepad(Joystick device)
        {
            this.device = device;
            previousButtonState = device.GetCurrentState().GetButtons();
            configuration = new GamepadConfiguration();

            actions = new Action[]
            {
                configuration.OnXDown, configuration.OnXUp,
                configuration.OnODown, configuration.OnOUp,
                configuration.OnSDown, configuration.OnSUp,
                configuration.OnTDown, configuration.OnTUp,
                configuration.OnL1Down, configuration.OnL1Up,
                configuration.OnR1Down, configuration.OnR1Up,
                configuration.OnSelectDown, configuration.OnSelectUp,
                configuration.OnStartDown, configuration.OnStartUp,
                configuration.OnL3Down, configuration.OnL3Up,
                configuration.OnR3Down, configuration.OnR3Up,
            };

            worker = new BackgroundWorker();
            timer.Elapsed += new ElapsedEventHandler(ManageInput);
            //worker.DoWork += worker_DoWork;

            timer.Interval = 10;
            timer.Enabled = true;

            worker.RunWorkerAsync();
        }

        private void ManageInput(object sender, ElapsedEventArgs e)
        {
            JoystickState state = device.GetCurrentState();
            bool[] curButtonState = state.GetButtons();

            for (int i = 0; i < actions.Length; i++)
            {
                if (curButtonState[i] && !previousButtonState[i])
                {
                    actions[i * 2]();
                }
                else if (!curButtonState[i] && previousButtonState[i])
                {
                    actions[i * 2 + 1]();
                }
            }
            previousButtonState = state.GetButtons();
        }
    }
}

using gamepad_mouse_controller.Model;
using SlimDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Timers;

namespace gamepad_mouse_controller.Controller
{
    class GamepadController
    {
        private IInput configuration;

        private readonly BackgroundWorker worker;
        private Timer timer = new Timer();

        public Joystick joystick;

        private bool[] previousButtonState;
        private Action[] actions;

        public GamepadController(IInput configuration, Joystick joystick)
        {
            this.configuration = configuration;
            this.joystick = joystick;
            previousButtonState = joystick.GetCurrentState().GetButtons();

            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;

            timer.Interval = 10;
            timer.Enabled = true;

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

            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            timer.Elapsed += new ElapsedEventHandler(ManageInput);
        }

        private void ManageInput(object sender, ElapsedEventArgs e)
        {
            JoystickState state = joystick.GetCurrentState();
            bool[] curButtonState = state.GetButtons();

            for (int i = 0; i < actions.Length; i++)
            {
                if(curButtonState[i] && !previousButtonState[i])
                {
                    actions[i * 2]();
                }
                else if(!curButtonState[i] && previousButtonState[i])
                {
                    actions[i * 2 + 1]();
                }
            }

            previousButtonState = state.GetButtons();
        }
    }
}

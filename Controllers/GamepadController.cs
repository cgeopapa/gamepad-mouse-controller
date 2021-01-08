using OpenTK.Input;
using System.Threading;
using System;
using gamepad_mouse_controller.Model;
using System.Collections.Generic;

namespace gamepad_mouse
{
    public delegate void OnControllerConnected(int index);
    partial class GamepadController
    {
        public static event OnControllerConnected onControllerConnected;
        public static List<Gamepad> gamepads { get; private set;} = new List<Gamepad>();

        private static int gamepadIndex = 0;
        private static Timer timer;

        public GamepadController()
        {
            GamePadState state = GamePad.GetState(gamepadIndex);
            while (state.IsConnected)
            {
                gamepads.Add(new Gamepad(gamepadIndex));
                onControllerConnected?.Invoke(gamepadIndex);
                state = GamePad.GetState(++gamepadIndex);
            }

            timer = new Timer(CheckForNewGamepad, new AutoResetEvent(false), 1, 3000);
        }

        ~GamepadController()
        {
            timer.Dispose();
        }

        private void CheckForNewGamepad(Object stateInfo)
        {
            GamePadState state = GamePad.GetState(gamepadIndex);
            if (state.IsConnected)
            {
                onControllerConnected?.Invoke(gamepadIndex);
                gamepadIndex++;
            }
            state = GamePad.GetState(gamepadIndex - 1);
            if (!state.IsConnected)
            {
                // Rase disconnected event
                gamepadIndex--;
            }
        }
    }
}

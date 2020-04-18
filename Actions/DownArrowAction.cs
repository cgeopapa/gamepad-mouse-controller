using gamepad_mouse_controller.Model;
using System;
using WindowsInput;
using WindowsInput.Native;

namespace gamepad_mouse_controller.Actions
{
    class DownArrowAction : IAction
    {
        private InputSimulator input = new InputSimulator();
        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            throw new NotImplementedException();
        }

        public void Execute(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void Execute(ActionArgs args)
        {
            if (args.down)
            {
                input.Keyboard.KeyDown(VirtualKeyCode.DOWN);
            }
            else
            {
                input.Keyboard.KeyUp(VirtualKeyCode.DOWN);
            }
        }
    }
}

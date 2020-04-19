using gamepad_mouse_controller.Model;
using System;
using WindowsInput;
using WindowsInput.Native;

namespace gamepad_mouse_controller.Actions
{
    class RightArrowAction : IAction
    {
        private InputSimulator input = new InputSimulator();
        public string Name => GetType().Name;

        public void Execute(ActionArgs args)
        {
            if (args.down)
            {
                input.Keyboard.KeyDown(VirtualKeyCode.RIGHT);
            }
            else
            {
                input.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
            }
        }
    }
}

using gamepad_mouse_controller.Model;
using System;
using WindowsInput;

namespace gamepad_mouse_controller.Actions
{
    class MouseLeftClickAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(ActionArgs args)
        {
            if (args.down)
            {
                input.Mouse.LeftButtonDown();
            }
            else
            {
                input.Mouse.LeftButtonUp();
            }
        }
    }
}

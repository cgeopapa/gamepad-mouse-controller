using gamepad_mouse_controller.Model;
using System;
using WindowsInput;

namespace gamepad_mouse_controller.Actions
{
    class MouseLeftClickAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            if (up)
            {
                input.Mouse.LeftButtonDown();
            }
            else
            {
                input.Mouse.LeftButtonUp();
            }
        }

        public void Execute(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void Execute(ActionArgs args)
        {
            if (args.up)
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

﻿using gamepad_mouse_controller.Model;
using WindowsInput;

namespace gamepad_mouse_controller.Actions
{
    class MouseMoveAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(ActionArgs args)
        {
            input.Mouse.MoveMouseBy(args.x, args.y);
        }
    }
}

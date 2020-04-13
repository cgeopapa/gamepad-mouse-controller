﻿using System;
using WindowsInput;

namespace gamepad_mouse_controller.Actions
{
    class MouseScrollAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            throw new NotImplementedException();
        }

        public void Execute(int x, int y)
        { 
            input.Mouse.HorizontalScroll(x);
            input.Mouse.VerticalScroll(y);
        }
    }
}
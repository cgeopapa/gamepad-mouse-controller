using System;
using WindowsInput;
using WindowsInput.Native;

namespace gamepad_mouse_controller.Actions
{
    class BrowserForwardAction : IAction
    {
        private InputSimulator input = new InputSimulator();
        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            if (up)
            {
                input.Keyboard.KeyDown(VirtualKeyCode.BROWSER_FORWARD);
            }
            else
            {
                input.Keyboard.KeyUp(VirtualKeyCode.BROWSER_FORWARD);
            }
        }

        public void Execute(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}

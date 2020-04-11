using System;
using WindowsInput;
using WindowsInput.Native;

namespace gamepad_mouse_controller.Actions
{
    class BrowserBackAction : IAction
    {
        private InputSimulator input = new InputSimulator();
        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            if (up)
            {
                input.Keyboard.KeyDown(VirtualKeyCode.BROWSER_BACK);
            }
            else
            {
                input.Keyboard.KeyUp(VirtualKeyCode.BROWSER_BACK);
            }
        }

        public void Execute(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}

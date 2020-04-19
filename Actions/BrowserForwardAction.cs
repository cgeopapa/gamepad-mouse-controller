using gamepad_mouse_controller.Model;
using WindowsInput;
using WindowsInput.Native;

namespace gamepad_mouse_controller.Actions
{
    class BrowserForwardAction : IAction
    {
        private InputSimulator input = new InputSimulator();
        public string Name => GetType().Name;

        public void Execute(ActionArgs args)
        {
            if (args.down)
            {
                input.Keyboard.KeyDown(VirtualKeyCode.BROWSER_FORWARD);
            }
            else
            {
                input.Keyboard.KeyUp(VirtualKeyCode.BROWSER_FORWARD);
            }
        }
    }
}

using gamepad_mouse_controller.Model;
using WindowsInput;
using WindowsInput.Native;

namespace gamepad_mouse_controller.Actions
{
    class WindowChangeAction : IAction
    {
        private InputSimulator input = new InputSimulator();
        public string Name => GetType().Name;

        public void Execute(ActionArgs args)
        {
            if(args.down)
            {
                input.Keyboard.KeyDown(VirtualKeyCode.MENU);
                input.Keyboard.KeyDown(VirtualKeyCode.TAB);
            }
            else
            {
                input.Keyboard.KeyUp(VirtualKeyCode.MENU);
                input.Keyboard.KeyUp(VirtualKeyCode.TAB);
            }
        }
    }
}

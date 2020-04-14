using gamepad_mouse_controller.Model;
using WindowsInput;
using WindowsInput.Native;

namespace gamepad_mouse_controller.Actions
{
    class WindowsKeyAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            if (up)
            {
                input.Keyboard.KeyDown(VirtualKeyCode.LWIN);
            }
            else
            {
                input.Keyboard.KeyUp(VirtualKeyCode.LWIN);
            }
        }

        public void Execute(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(ActionArgs args)
        {
            if (args.up)
            {
                input.Keyboard.KeyDown(VirtualKeyCode.LWIN);
            }
            else
            {
                input.Keyboard.KeyUp(VirtualKeyCode.LWIN);
            }
        }
    }
}

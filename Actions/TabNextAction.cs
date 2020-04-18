using gamepad_mouse_controller.Model;
using WindowsInput;
using WindowsInput.Native;

namespace gamepad_mouse_controller.Actions
{
    class TabNextAction : IAction
    {
        private InputSimulator input = new InputSimulator();
        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(ActionArgs args)
        {
            if (args.down)
            {
                input.Keyboard.KeyDown(VirtualKeyCode.TAB);
            }
            else
            {
                input.Keyboard.KeyUp(VirtualKeyCode.TAB);
            }
        }
    }
}

using gamepad_mouse_controller.Model;
using WindowsInput;

namespace gamepad_mouse_controller.Actions
{
    class MouseRightClickAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(ActionArgs args)
        {
            if (args.down)
            {
                input.Mouse.RightButtonDown();
            }
            else
            {
                input.Mouse.RightButtonUp();
            }
        }
    }
}

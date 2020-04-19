using gamepad_mouse_controller.Model;
using WindowsInput;

namespace gamepad_mouse_controller.Actions
{
    class MouseScrollAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(ActionArgs args)
        {
            input.Mouse.HorizontalScroll(args.x);
            input.Mouse.VerticalScroll(args.y);
        }
    }
}

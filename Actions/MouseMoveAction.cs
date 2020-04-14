using gamepad_mouse_controller.Model;
using WindowsInput;

namespace gamepad_mouse_controller.Actions
{
    class MouseMoveAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(int x, int y)
        {
            input.Mouse.MoveMouseBy(x, y);
        }

        public void Execute(ActionArgs args)
        {
            input.Mouse.MoveMouseBy(args.x, args.y);
        }
    }
}

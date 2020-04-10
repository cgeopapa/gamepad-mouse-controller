using WindowsInput;

namespace gamepad_mouse_controller.Actions
{
    class MouseRightClickAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            if (up)
            {
                input.Mouse.RightButtonDown();
            }
            else
            {
                input.Mouse.RightButtonUp();
            }
        }

        public void Execute(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}

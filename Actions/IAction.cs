namespace gamepad_mouse_controller.Actions
{
    interface IAction
    {
        public string Name { get; }
        public void Execute(bool up);
        public void Execute(int x, int y);
    }
}

namespace gamepad_mouse_controller.Actions
{
    interface IAction
    {
        string Name { get; }
        void Execute(bool up);
        void Execute(int x, int y);
    }
}

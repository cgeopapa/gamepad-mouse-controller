namespace gamepad_mouse_controller.Model
{
    interface IAction
    {
        string Name { get; }
        void Execute(ActionArgs args);
    }
}

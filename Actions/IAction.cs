using gamepad_mouse_controller.Model;

namespace gamepad_mouse_controller.Actions
{
    interface IAction
    {
        string Name { get; }
        void Execute(ActionArgs args);
    }
}

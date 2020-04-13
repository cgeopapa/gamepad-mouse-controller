using WindowsInput;
using WindowsInput.Native;

namespace gamepad_mouse_controller.Actions
{
    class ShowSettingsAction : IAction
    {
        private InputSimulator input = new InputSimulator();

        public string Name => GetType().Name;

        public void Execute(bool up)
        {
            if(!up)
            {
                
            }
        }

        public void Execute(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}

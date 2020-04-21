using gamepad_mouse_controller.Model;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gamepad_mouse_controller.Actions
{
    class EnableDisableGamepadAction : IAction
    {
        private static bool isAsking = false;

        public string Name => GetType().Name;

        public void Execute(ActionArgs args)
        {
            if (!isAsking)
            {
                isAsking = true;
                if (args.down)
                {
                    Task.Run(() =>
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to disable your gamepad?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        if (result == DialogResult.Yes)
                        {
                            args.gamepad.ChangeState();
                        }
                        isAsking = false;
                    });
                }
            }
        }
    }
}

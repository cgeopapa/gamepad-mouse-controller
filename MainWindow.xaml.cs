using gamepad_mouse_controller.Controllers;
using System.Windows;

namespace gamepad_mouse_controller
{
    public partial class MainWindow : Window
    {
        private readonly GamepadController gamepadController = new GamepadController();

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}

using gamepad_mouse_controller.Controller;
using gamepad_mouse_controller.Actions;
using System.Windows;
using System.Collections.Generic;

namespace gamepad_mouse_controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GamepadController gamepadController = new GamepadController();

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}

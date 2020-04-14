using gamepad_mouse_controller.Model;
using System.ComponentModel;
using System.Windows;

namespace gamepad_mouse_controller
{
    public partial class GamepadSettingsWindow : Window
    {
        private Gamepad gamepad;
        public GamepadSettingsWindow(Gamepad gamepad)
        {
            InitializeComponent();
            Hide();
            this.gamepad = gamepad;
        }

        public void ShowWindow()
        {
            this.Dispatcher.Invoke(() =>
            {
                Show();

                content.Content = gamepad.index;
            });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            Hide();

            base.OnClosing(e);
        }
    }
}

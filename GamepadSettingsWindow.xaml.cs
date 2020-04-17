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
            mouseDockPanel.Style = FindResource("PanelBackground") as Style;
            scrollDockPanel.Style = FindResource("PanelBackground") as Style;
            Hide();
            this.gamepad = gamepad;
        }

        public void ShowWindow()
        {
            this.Dispatcher.Invoke(() =>
            {
                Show();

                Title = string.Format("Controller {0} Settings", gamepad.index);
                mouseSesitivitySlider.Value = gamepad.mouseSensitivity;
                scrollSesitivitySlider.Value = gamepad.scrollSensitivity;
            });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            Hide();

            base.OnClosing(e);
        }

        private void mouseSesitivitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            gamepad.mouseSensitivity = (float)mouseSesitivitySlider.Value + .2f;
        }

        private void scrollSesitivitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            gamepad.scrollSensitivity = (int)scrollSesitivitySlider.Value;
        }
    }
}

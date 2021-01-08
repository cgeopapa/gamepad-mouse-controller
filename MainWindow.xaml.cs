using System.ComponentModel;
using System.Windows;
using MahApps.Metro.Controls;
using gamepad_mouse_controller.Model;
using System.Diagnostics;
using gamepad_mouse;

namespace gamepad_mouse_controller
{
    public partial class MainWindow : MetroWindow
    {
        private readonly GamepadController gamepadController = new GamepadController();

        public MainWindow()
        {
            InitializeComponent();
            gamepadList.ItemsSource = GamepadController.gamepads;
            Hide();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            Hide();

            base.OnClosing(e);
        }

        public void ShowSettings()
        {
            Show();
            WindowState = WindowState.Normal;
        }

        public static void Refresh(Gamepad gamepad)
        {
            
        }

        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to turn off the system?", "Turn off the system", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                Process.Start("shutdown", "/s /t 0");
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to restart the system?", "Restart the system", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Process.Start("shutdown", "/r /t 0");
            }
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TaskbarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ShowSettings();
        }
    }
}

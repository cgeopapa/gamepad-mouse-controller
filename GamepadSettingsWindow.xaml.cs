using gamepad_mouse_controller.Model;
using System.ComponentModel;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Input;

namespace gamepad_mouse_controller
{
    public partial class GamepadSettingsWindow : MetroWindow
    {
        private Gamepad gamepad;
        private bool visible = false;

        public GamepadSettingsWindow(Gamepad gamepad)
        {
            this.gamepad = gamepad;

            InitializeComponent();
            PreviewKeyDown += MainWindowPreviewKeyDown;

            mouseDockPanel.Style = FindResource("PanelBackground") as Style;
            scrollDockPanel.Style = FindResource("PanelBackground") as Style;

            mouseSesitivitySlider.Value = this.gamepad.mouseSensitivity;
            mouseSesitivitySlider.ValueChanged += mouseSesitivitySlider_ValueChanged;
            scrollSesitivitySlider.Value = this.gamepad.scrollSensitivity;
            scrollSesitivitySlider.ValueChanged += scrollSesitivitySlider_ValueChanged;

            Hide();
        }

        public void ShowWindow()
        {
            this.Dispatcher.Invoke(() =>
            {
                if (!visible)
                {
                    Show();

                    Title = string.Format("Controller {0} Settings", gamepad.Index);
                    mouseSesitivitySlider.Value = gamepad.mouseSensitivity;
                    scrollSesitivitySlider.Value = gamepad.scrollSensitivity;
                    Activate();

                    visible = true;
                }
                else
                {
                    Hide();
                    visible = false;
                }
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
            gamepad.mouseSensitivity = (float)mouseSesitivitySlider.Value;
        }

        private void scrollSesitivitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            gamepad.scrollSensitivity = (float)scrollSesitivitySlider.Value;
        }

        static void MainWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
            }
        }

        private void Slider_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                keyboardFocus.MoveFocus(tRequest);
                
                e.Handled = true;
            }
            else if(e.Key == Key.Down)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

                keyboardFocus.MoveFocus(tRequest);

                e.Handled = true;
            }
        }

        private void MetroWindow_Deactivated(object sender, System.EventArgs e)
        {
            ShowWindow();
        }
    }
}

using gamepad_mouse_controller.Controller;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;

namespace gamepad_mouse_controller
{
    public partial class MainWindow : Window
    {
        private readonly GamepadController gamepadController = new GamepadController();

        public MainWindow()
        {
            InitializeComponent();
            Hide();

            NotifyIcon ni = new NotifyIcon
            {
                Icon = new System.Drawing.Icon("Main.ico"),
                Visible = true
            };
            ni.DoubleClick += ShowSettings;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            Hide();

            base.OnClosing(e);
        }

        public void ShowSettings(object sender, EventArgs args)
        {
            Show();
            WindowState = WindowState.Normal;
        }
    }
}

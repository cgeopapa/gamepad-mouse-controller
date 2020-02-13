using System.Windows;

namespace gamepad_mouse_controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ControllerSearch controllerSearchWorker;

        public MainWindow()
        {
            InitializeComponent();
            controllerSearchWorker = new ControllerSearch();
        }
    }
}

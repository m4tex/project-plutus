using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using project_plutus_backend;

namespace project_plutus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static BackendApplication _app = new();
        private static ViewModel _viewModel = new(_app);

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = _viewModel;
            
        }

        private void TakeToMarketButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
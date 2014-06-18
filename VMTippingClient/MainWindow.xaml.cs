using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VMTipping.Model;

namespace VMTippingClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void ShowUserPrediction(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((DataGrid) sender).SelectedItem;
            if (selectedItem != null)
            {
                //var window = new UserPredictionWindow();
                //window.DataContext = new UserPredictionViewModel(selectedItem as User);
                //window.Show();
            }
            
        }
    }
}

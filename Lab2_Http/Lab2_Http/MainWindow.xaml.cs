using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2_Http
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnFetchData_Click(object sender, RoutedEventArgs e)
        {
            string uri = txtURL.Text;
            if (string.IsNullOrEmpty(uri))
            {
                MessageBox.Show("Please enter a valid URL.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                MessageBox.Show("Fetching data...");
                string responseBody = await client.GetStringAsync(uri);
                txtContent.Text = responseBody.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to fetch data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

      

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtURL.Text = string.Empty;
            txtContent.Text = string.Empty;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
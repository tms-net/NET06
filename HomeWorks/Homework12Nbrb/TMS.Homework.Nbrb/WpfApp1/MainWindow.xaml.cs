using APILibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private APIClient _apiClient;

        public MainWindow()
        {
            InitializeComponent();
            _apiClient = new APIClient();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var currencies = await _apiClient.GetShortCurrenciesAsync();
            txtCurrencied.Text = string.Join("\n", currencies.Select(c => c.Name));
        }
    }
}

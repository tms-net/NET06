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
        private readonly APIClient _apiClient;

        public MainWindow()
        {
            InitializeComponent();
            _apiClient = new APIClient();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
	        txtCurrencies.Text = "loading currencies...";
            var currencies = await _apiClient.GetShortCurrenciesAsync();
            txtCurrencies.Text = string.Join("\t", currencies.Select(c => $"{c.Name} - {c.Code}"));
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
	        txtCurrencies.Text = "loading rates...";
            var rates = await _apiClient.GetRatesAsync(DateTime.Now.AddDays(-2), DateTime.Now, 145);
            txtCurrencies.Text = $"results for: 145:\n";
            //await Task.Delay(100);
            txtCurrencies.Text += string.Join("\t", rates.Select(r => r.Cur_OfficialRate));
        }
    }
}

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

namespace DNS1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProdagiPage.xaml
    /// </summary>
    public partial class ProdagiPage : Page
    {
        public ProdagiPage()
        {
            InitializeComponent();
            UpdateServices();
        }
        public ProdagiPage(Entities.Prodagi service)
        {
            InitializeComponent();
            LViewServices.ItemsSource = App.Context.Tovars.ToList();

        }
        //Окно редактирования 
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Tovar;
            NavigationService.Navigate(new AddEditProdagi(currentService));
            UpdateServices();
        }

        private void LViewServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }
        private void UpdateServices()
        {
            var services = App.Context.Tovars.ToList();
            if (ComboSortBy.SelectedIndex == 0)
                services = services.OrderBy(p => p.Obsh_pribil).ToList();
            else
                services = services.OrderByDescending(p => p.Obsh_pribil).ToList();

            services = services.Where(p => p.PribilOtTovata.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewServices.ItemsSource = services;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }
    }
}

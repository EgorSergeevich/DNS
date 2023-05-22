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
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public Products()
        {
            InitializeComponent();
            UpdateServices();
        }
        
        public Products(Entities.Tovar service)
        {
            InitializeComponent();
            LViewServices.ItemsSource = App.Context.Tovars.ToList();
            if (App.CurrentUser.id_role == 2)
                BtnAddTovar.Visibility = Visibility.Hidden;
        }
        //Добавление нового товара
        private void BtnAddTovar_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AddEditTovar());
            UpdateServices();
        }
        //Окно редактирования
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Tovar;
            NavigationService.Navigate(new AddEditTovar(currentService));
            UpdateServices();
        }
        //Удаление уже существующего товара
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Tovar;

            if (MessageBox.Show($"Вы уверены, что хотите удалить Товар: " + $"{currentService.Nasvanie_tovara}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Tovars.Remove(currentService);
                App.Context.SaveChanges();
                UpdateServices();
            }
        }
        private void LViewServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
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
                services = services.OrderBy(p => p.Stoimost).ToList();
            else
                services = services.OrderByDescending(p => p.Stoimost).ToList();

            services = services.Where(p => p.KolichestvoTovara.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            LViewServices.ItemsSource = services;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }
    }
}

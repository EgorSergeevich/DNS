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
    /// Логика взаимодействия для PersonalPage.xaml
    /// </summary>
    public partial class PersonalPage : Page
    {
        public PersonalPage()
        {
            InitializeComponent();
            UpdateServices();
        }
        public PersonalPage(Entities.Sotrydniki service)
        {
            InitializeComponent();
            LViewServices.ItemsSource = App.Context.Sotrydnikis.ToList();
            if (App.CurrentUser.id_role == 2)
                BtnAddSotrydnik.Visibility = Visibility.Hidden;
        }
        //Окно добавления сотрудников 
        private void BtnAddSotrydnik_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPersonal());
            UpdateServices();
        }
        //Ректирование информации
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Sotrydniki;
            NavigationService.Navigate(new AddEditPersonal(currentService));
            UpdateServices();
        }
        //Удаление уже существующего сотрудника
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Sotrydniki;

            if (MessageBox.Show($"Вы уверены, что хотите удалить Сотрудника: " + $"{currentService.F} {currentService.I} {currentService.O}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.Context.Sotrydnikis.Remove(currentService);
                App.Context.SaveChanges();
                UpdateServices();
            }
        }
        private void LViewServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            var services = App.Context.Sotrydnikis.ToList();
            if (ComboSortBy.SelectedIndex == 0)
                services = services.OrderBy(p => p.F).ToList();
            else
                services = services.OrderByDescending(p => p.F).ToList();

            services = services.Where(p => p.FIO.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            LViewServices.ItemsSource = services;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }
    }
}

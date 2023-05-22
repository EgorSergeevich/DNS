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
    /// Логика взаимодействия для Dolshnosti.xaml
    /// </summary>
    public partial class Dolshnosti : Page
    {
        public Dolshnosti(Entities.Dolshnosti service)
        {
            InitializeComponent();
            LViewServices.ItemsSource = App.Context.Dolshnostis.ToList();
            if (App.CurrentUser.id_role == 2)
                BtnAddDoljnosti.Visibility = Visibility.Hidden;
        }
        private void UpdateServices()
        {
            var services = App.Context.Dolshnostis.ToList();
            if (ComboSortBy.SelectedIndex == 0)
                services = services.OrderBy(p => p.Oklad).ToList();
            else
                services = services.OrderByDescending(p => p.Oklad).ToList();

            services = services.Where(p => p.NazvanieOklada.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            LViewServices.ItemsSource = services;
        }
        //Редактирование данных
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Dolshnosti;
            NavigationService.Navigate(new AddEditDolshnosti(currentService));
            UpdateServices();
        }
        //Удаление существующей должности
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentService = (sender as Button).DataContext as Entities.Dolshnosti;

            if (MessageBox.Show($"Вы уверены, что хотите удалить Должность: " + $"{currentService.Nazvanie}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                Entities.Sotrydniki id = App.Context.Sotrydnikis.Where(c => c.id_dolshnosti == currentService.id_dolshnosti).FirstOrDefault();
                while (id != null)
                {
                    if (id != null)
                    {
                        id.id_dolshnosti = null;
                        id = App.Context.Sotrydnikis.Where(c => c.id_dolshnosti == currentService.id_dolshnosti).FirstOrDefault();
                        App.Context.SaveChanges();
                    }
                }
                App.Context.Dolshnostis.Remove(currentService);
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


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }
        //Добавление новой должности
        private void BtnAddDoljnosti_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditDolshnosti());
            UpdateServices();
        }
    }
}

using DNS1.Entities;
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
    /// Логика взаимодействия для Glavnaya.xaml
    /// </summary>
    public partial class Glavnaya : Page
    {
        public Glavnaya()
        {
            InitializeComponent();
            //Не дает увеличить размер окна
            App.Current.MainWindow.ResizeMode = ResizeMode.CanMinimize; 
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        //Окно товары
        private void BtnTovar_Click(object sender, RoutedEventArgs e)
        {
            var currentServic = (sender as Button).DataContext as Entities.Tovar;
            NavigationService.Navigate(new Products(currentServic));
        }
        //Окно сотрудники
        private void BtnSotrydniki_Click(object sender, RoutedEventArgs e)
        {
            var currentServic = (sender as Button).DataContext as Entities.Sotrydniki;
            NavigationService.Navigate(new PersonalPage(currentServic));
        }
        //Окно продажи
        private void BtnProdagi_Click(object sender, RoutedEventArgs e)
        {
            var currentServic = (sender as Button).DataContext as Entities.Prodagi;
            NavigationService.Navigate(new ProdagiPage(currentServic));
        }
        //Окно должности
        private void BtnDoljnost_Click(object sender, RoutedEventArgs e)
        {
            var currentServic = (sender as Button).DataContext as Entities.Dolshnosti;
            NavigationService.Navigate(new Dolshnosti(currentServic));
        } 
    }
}

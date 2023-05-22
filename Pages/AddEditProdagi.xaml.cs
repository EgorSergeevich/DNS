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
    /// Логика взаимодействия для AddEditProdagi.xaml
    /// </summary>
    public partial class AddEditProdagi : Page
    {
        private Entities.Tovar _currentService = null;
        public AddEditProdagi()
        {
            InitializeComponent();
        }
        public AddEditProdagi(Entities.Tovar service)
        {
            InitializeComponent();

            _currentService = service;
            Title = "Редактировать";
            TBoxNazvanie.Text = _currentService.Nasvanie_tovara;
            TBoxKolichestvo.Text = _currentService.Kol_prod_tovara.ToString();
            TBoxPribil.Text = _currentService.Obsh_pribil.ToString();
            try
            {
                TBoxPribil.Text = (TBoxPribil.Text.Remove(service.Obsh_pribil.ToString().IndexOf(","), 5));
            }
            catch (Exception)
            { }
        }
        //Сохранение введенных данных
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (_currentService == null)
                {
                    var tovar = new Entities.Tovar
                    {
                        Nasvanie_tovara = TBoxNazvanie.Text,
                    };
                    App.Context.Tovars.Add(tovar);
                    App.Context.SaveChanges();

                    var tovar2 = new Entities.Prodagi
                    {
                        Kol_prod_tovara = int.Parse(TBoxKolichestvo.Text),
                        Obsh_pribil = _currentService.Stoimost * int.Parse(TBoxKolichestvo.Text),
                    };
                    App.Context.Prodagis.Add(tovar2);
                    App.Context.SaveChanges();
                }
                else
                {
                    _currentService.Nasvanie_tovara = TBoxNazvanie.Text;
                    _currentService.Kol_prod_tovara = int.Parse(TBoxKolichestvo.Text);
                    _currentService.Obsh_pribil = _currentService.Stoimost * (int.Parse(TBoxKolichestvo.Text));
                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            // Поля для обязательного заполнения
            if (string.IsNullOrWhiteSpace(TBoxNazvanie.Text))
                errorBuilder.AppendLine("Название товара обязательно для заполнения;");

            if (string.IsNullOrWhiteSpace(TBoxKolichestvo.Text))
                errorBuilder.AppendLine("Количество проданного товара обязательно для заполнения;");
            int discount = 0;
            if (int.TryParse(TBoxKolichestvo.Text, out discount) == false
                || discount < 0 || discount > 20)
            {
                errorBuilder.AppendLine("Кол-во товаров - целое положительное число в диапазоне от 0 до 20");
            }

            return errorBuilder.ToString();
        }


        private void TBoxNazvanie_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

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
    /// Логика взаимодействия для AddEditDolshnosti.xaml
    /// </summary>
    public partial class AddEditDolshnosti : Page
    {
        private Entities.Dolshnosti _currentService = null;
        public AddEditDolshnosti()
        {
            InitializeComponent();
        }
        public AddEditDolshnosti(Entities.Dolshnosti service)
        {
            InitializeComponent();

            _currentService = service;
            Title = "Редактировать";
            TBoxNazvanieDoljnosti.Text = _currentService.Nazvanie;
            TBoxZarplata.Text = _currentService.Oklad.ToString();
        }
        //Кнопка сохранить
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
                    var service = new Entities.Dolshnosti
                    {
                        Nazvanie = TBoxNazvanieDoljnosti.Text,
                        Oklad = decimal.Parse(TBoxZarplata.Text),
                    };
                    App.Context.Dolshnostis.Add(service);
                    App.Context.SaveChanges();
                }
                else
                {
                    _currentService.Nazvanie = TBoxNazvanieDoljnosti.Text;
                    _currentService.Oklad = decimal.Parse(TBoxZarplata.Text);
                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            // Поля для обязательного заполнения
            if (string.IsNullOrWhiteSpace(TBoxNazvanieDoljnosti.Text))
                errorBuilder.AppendLine("Название должности обязательно для заполнения;");

            if (string.IsNullOrWhiteSpace(TBoxZarplata.Text))
                errorBuilder.AppendLine("Зарплата обязательна для заполнения;");

            var doljnostiFromDB = App.Context.Dolshnostis.ToList()
                .FirstOrDefault(p => p.Nazvanie.ToLower() == TBoxNazvanieDoljnosti.Text.ToLower());
            if (doljnostiFromDB != null && doljnostiFromDB != _currentService)
                errorBuilder.AppendLine("Такая должность уже есть в базе данных;");

            decimal cost = 0;
            if (decimal.TryParse(TBoxZarplata.Text, out cost) == false
                || cost <= 0)
                errorBuilder.AppendLine("Зарплата вводимое значение должно быть положительным числом;");


            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }

        private void TBoxNazvanie_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

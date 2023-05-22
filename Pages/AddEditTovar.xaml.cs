using Microsoft.Win32;
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
using System.IO;

namespace DNS1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditTovar.xaml
    /// </summary>
    public partial class AddEditTovar : Page
    {
        private Entities.Tovar _currentService = null;
       
        public AddEditTovar()
        {
            InitializeComponent();
        }
        public AddEditTovar(Entities.Tovar service)
        {
            InitializeComponent();

            _currentService = service;
            Title = "Редактировать услугу";
            TBoxNazvanie.Text = _currentService.Nasvanie_tovara;
            TBoxStoimost.Text = _currentService.Stoimost.ToString();
            TBoxKolichestvo.Text = _currentService.Kolichestvo.ToString();
            TBoxOpisanie.Text = _currentService.Opisanie;
        }


        private byte[] _mainImageData;

        //Выбор мзображения
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog foto = new OpenFileDialog();
            foto.Filter = "Image | *.png; *.jpg; *.jpeg;";
            if (foto.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(foto.FileName);
                Imagephototovara.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
            }
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
                    var service = new Entities.Tovar
                    {
                        Nasvanie_tovara = TBoxNazvanie.Text,
                        Stoimost = decimal.Parse(TBoxStoimost.Text),
                        Kolichestvo = int.Parse(TBoxKolichestvo.Text),
                    };
                    App.Context.Tovars.Add(service);
                    App.Context.SaveChanges();
                    MessageBox.Show("Товар успешно добавлен!");
                }
                else
                {
                    _currentService.Nasvanie_tovara = TBoxNazvanie.Text;
                    _currentService.Stoimost = decimal.Parse(TBoxStoimost.Text);
                    _currentService.Kolichestvo = int.Parse(TBoxKolichestvo.Text);
                    _currentService.Opisanie = TBoxOpisanie.Text;
                    if (_mainImageData != null)
                        _currentService.Imagephototovara = _mainImageData;
                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
                MessageBox.Show(" Изменения сохранены!");
            }
        }
        //Кнопка печати
        private void ButtPechat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.PagePechat(TBoxNazvanie.Text, TBoxKolichestvo.Text, TBoxStoimost.Text, TBoxOpisanie.Text));
        }

        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            // Поля для обязательного заполнения
            if (string.IsNullOrWhiteSpace(TBoxNazvanie.Text))
                errorBuilder.AppendLine("Название товара обязательно для заполнения;");

            var serviceFromDB = App.Context.Tovars.ToList()
                .FirstOrDefault(p => p.Nasvanie_tovara.ToLower() == TBoxNazvanie.Text.ToLower());
            if (serviceFromDB != null && serviceFromDB != _currentService)
                errorBuilder.AppendLine("Такой товар уже есть в базе данных;");

            if (string.IsNullOrWhiteSpace(TBoxStoimost.Text))
                errorBuilder.AppendLine("Стоимость товара обязательно для заполнения;");

            decimal cost = 0;
            if (decimal.TryParse(TBoxStoimost.Text, out cost) == false
                || cost <= 0)
                errorBuilder.AppendLine("Стоимость товара должно быть положительным числом;");
            if (string.IsNullOrWhiteSpace(TBoxKolichestvo.Text))
                errorBuilder.AppendLine("Количество товара обязательно для заполнения;");
            if (!string.IsNullOrEmpty(TBoxKolichestvo.Text))
            {
                int discount = 0;
                if (int.TryParse(TBoxKolichestvo.Text, out discount) == false
                    || discount < 0 || discount > 100)
                {
                    errorBuilder.AppendLine("Кол-во товаров - целое положительное число в диапазоне от 0 до 100");
                }
            }

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
        private void TBoxNazvanie_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEditPersonal.xaml
    /// </summary>
    public partial class AddEditPersonal : Page
    {
        public AddEditPersonal()
        {
            InitializeComponent();
        }
        private Entities.Sotrydniki _currentService = null;
        
        public AddEditPersonal(Entities.Sotrydniki service)
        {
            InitializeComponent();

            _currentService = service;
            Title = "Редактировать";
            TBoxFamilia.Text = _currentService.F;
            TBoxImya.Text = _currentService.I;
            TBoxOtchestvo.Text = _currentService.O;
            TBoxTelefon.Text = _currentService.Telefon;
            TBoxDataPrinatiyaNaRaboty.Text = _currentService.Data_na_rab.ToString();
        }
        private byte[] _mainImageData;
        //Кнопка выбора изображения
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
                    Entities.Sotrydniki sotr = new Entities.Sotrydniki();
                    sotr.I = TBoxImya.Text;
                    sotr.F = TBoxFamilia.Text;
                    sotr.O = TBoxOtchestvo.Text;
                    sotr.Telefon = TBoxTelefon.Text;
                    sotr.Data_na_rab = DateTime.Parse(TBoxDataPrinatiyaNaRaboty.Text);

                    App.Context.Sotrydnikis.Add(sotr);

                    App.Context.SaveChanges();
                    MessageBox.Show("Cотрудник успешно добавлен!");

                }
                else
                {
                    Entities.Sotrydniki sotr = _currentService;
                    sotr.I = TBoxImya.Text;
                    sotr.F = TBoxFamilia.Text;
                    sotr.O = TBoxOtchestvo.Text;
                    sotr.Telefon = TBoxTelefon.Text;
                    sotr.Data_na_rab = DateTime.Parse(TBoxDataPrinatiyaNaRaboty.Text);
                    if (_mainImageData != null)
                        _currentService.imagephotosotr = _mainImageData;
                    App.Context.SaveChanges();
                }
                NavigationService.GoBack();
                MessageBox.Show("Изменения сохранены!");
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            // Поля для обязательного заполнения

            if (string.IsNullOrWhiteSpace(TBoxFamilia.Text))
                errorBuilder.AppendLine("Фамилия обязательна для заполнения;");

            if (string.IsNullOrWhiteSpace(TBoxImya.Text))
                errorBuilder.AppendLine("Имя обязательна для заполнения;");

            if (string.IsNullOrWhiteSpace(TBoxOtchestvo.Text))
                errorBuilder.AppendLine("Отчество обязательна для заполнения;");

            if (string.IsNullOrWhiteSpace(TBoxDataPrinatiyaNaRaboty.Text))
                errorBuilder.AppendLine("Дата начала работы обязательна для заполнения;");

            if (string.IsNullOrWhiteSpace(TBoxTelefon.Text))
                errorBuilder.AppendLine("Телефон обязателен для заполнения;");

            decimal cost = 0;
            if (decimal.TryParse(TBoxTelefon.Text, out cost) == false
                || cost <= 0)
                errorBuilder.AppendLine("Телефон должен быть в цифровом формате;");

            var sotrydnikiFromDB = App.Context.Sotrydnikis.ToList()
                .FirstOrDefault(p => p.F.ToLower() == TBoxFamilia.Text.ToLower());
            if (sotrydnikiFromDB != null && sotrydnikiFromDB != _currentService)
                errorBuilder.AppendLine("Такой сотрудник уже есть в базе данных;");

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }

        private void TBoxNazvanie_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

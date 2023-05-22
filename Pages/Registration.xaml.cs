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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на наличие логина в базе
            var test = App.Context.Users.Where(p => p.Login.ToLower().Contains(TBoxLogin.Text.ToLower())).FirstOrDefault();

            if (test != null)
                MessageBox.Show("Такой пользователь уже есть в базе данных;");
            else if (test == null)
            {
                MessageBox.Show("Логин или пароль введены неккоректно или не введены вовсе.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (App.Context.Users == null)
            {
                MessageBox.Show("Введите логин", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Entities.User sotr = new Entities.User();
                sotr.Login = TBoxLogin.Text;
                sotr.Password = PBoxPassword.Password;
                sotr.id_role = 2;
                App.Context.Users.Add(sotr);
                App.Context.SaveChanges();
                NavigationService.GoBack();
                MessageBox.Show("Пользователь зарегистрирован!");
            }
        }
    }
}

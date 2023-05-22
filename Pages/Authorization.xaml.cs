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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        
        public Authorization()
        {
            InitializeComponent();
            try
            {
                // Пробуем присоединится к БД, если возникает ошибка, обрабатываем ошибку SqlClient.SqlException
                App.Context.Database.Connection.Open();
                // Если присоединились, то пробуем закрыть соединение.
                App.Context.Database.Connection.Close(); 
            }
            catch (System.Data.SqlClient.SqlException es)
            {
                MessageBox.Show($"Ошибка подключения к базе данных:\n{es.Message}\nПриложение будет закрыто.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
            catch (Exception es)
            {
                MessageBox.Show($"Возникла непредвиденная ошибка, сообщение ошибки:\n{es.Message}\nПриложение будет закрыто.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
        }
        //Кнопка войти
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Entities.User currentUser = App.Context.Users.Where(p => p.Login == TBoxLogin.Text && p.Password == PBoxPassword.Password).FirstOrDefault();
            if (currentUser != null)
            {
                App.CurrentUser = App.Context.Users.Where(p => p.Login == TBoxLogin.Text && p.Password == PBoxPassword.Password).FirstOrDefault();
                NavigationService.Navigate(new Glavnaya());
            }
            else
            {
                MessageBox.Show("Пользователь не найден, логин или пароль введены неккоректно или не введены вовсе.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Регистрация нового пользователя
        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Registration());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DNS1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.DNS1Entities4 Context
        { get; } = new Entities.DNS1Entities4();
        public static Entities.User CurrentUser = null;
    }
}

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
using System.Printing;
using System.Xml.Linq;
using DNS1.Entities;

namespace DNS1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PagePechat.xaml
    /// </summary>
    public partial class PagePechat : Page
    {
        private Saka currentServic;


        public PagePechat(Saka currentServic)
        {
            this.currentServic = currentServic;
        }


        public PagePechat( string nazvanie, string kolichestvo, string stoimost, string opisanie)
        {
            InitializeComponent();
            LabelNazvanie.Content = nazvanie;
            LabelKolichestvo.Content = kolichestvo;
            LabelStoimost.Content = stoimost;
            LabelOpisanie.Content = opisanie;
        }

        public static void Print(Visual elementToPrint, string description)
        {
            using (var printServer = new LocalPrintServer())
            {
                var dialog = new PrintDialog();
                var qs = printServer.GetPrintQueues();
                dialog.PrintTicket.PageOrientation = PageOrientation.Portrait;
                dialog.PrintVisual(elementToPrint, description);

            }

        }
        //Формирование PDF файла
        private void ButtonPechat_Click(object sender, RoutedEventArgs e)
        {
            ButtonPechat.Visibility = Visibility.Hidden;
            Print(this, Convert.ToString(LabelNazvanie.Content));
        }

        
    }
}

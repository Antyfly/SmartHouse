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
using static SmartHouse.Entity.AppData;

namespace SmartHouse.Frame
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public string Surname, NameUser, Patronymic;
        public int _user;
        public Profile(int ID)
        {
            _user = ID;
            InitializeComponent();
            InfoLK();
            
        }
        public void InfoLK()
        {
            Download();

            LBSurname.Content = Surname + " " + NameUser + " " + Patronymic;
            //LBName.Content = NameUser;
            //LBPatronymic.Content = Patronymic;
        }

        public void Download()
        {
            IEnumerable<string> Familia =
                    from Users in context.Users
                    where Users.IDUsers == _user
                    select Users.Surname;
             Surname = Familia.First();

            IEnumerable<string> Imia =
                    from Users in context.Users
                    where Users.IDUsers == _user
                    select Users.Name;
             NameUser = Imia.First();

            IEnumerable<string> Otchesstvo =
                    from Users in context.Users
                    where Users.IDUsers == _user
                    select Users.Patronymic;
            Patronymic = Otchesstvo.First();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(DarkWindow))
                {
                    (window as DarkWindow).Close();
                }
            }
        }
    }
}

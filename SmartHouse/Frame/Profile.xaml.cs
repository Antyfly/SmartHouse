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
        public string Surname, NameUser, Patronymic, Email, Phone, Login, KeyWord;

        private void ChangeInfoLKButton_Click(object sender, RoutedEventArgs e)
        {

        }

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
            LBFio.Content = Surname + " " + NameUser + " " + Patronymic;
            LBEmail.Content = Email;
            LBPhone.Content = Phone;
            LBLogin.Content = Login;
            LBWord.Content = KeyWord;
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

            IEnumerable<string> Pochta =
                   from Users in context.Users
                   where Users.IDUsers == _user
                   select Users.Email;
            Email = Pochta.First();

            IEnumerable<string> Telephone =
                   from Users in context.Users
                   where Users.IDUsers == _user
                   select Users.phone;
            Phone = Telephone.First();

            IEnumerable<string> login =
                  from UserLogins in context.UserLogins
                  where UserLogins.IDUsers == _user
                  select UserLogins.LoginProvider;
            Login = login.First();

            IEnumerable<string> word =
                  from UserLogins in context.UserLogins
                  where UserLogins.IDUsers == _user
                  select UserLogins.KeyWord;
            KeyWord = word.First();
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

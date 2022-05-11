using SmartHouse.Entity;
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
    /// Логика взаимодействия для ChangeProfile.xaml
    /// </summary>
    public partial class ChangeProfile : Page
    {
        public string Surname, NameUser, Patronymic, Email, Phone, Login, KeyWord;
        private int _IdUser;

      
        public ChangeProfile( int IDUser)
        {
            InitializeComponent();
            _IdUser = IDUser;
            InfoLK();
        }

        private void ChangeInfoLKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IEnumerable<UserLogins> userLogins = context.UserLogins.Where(x => x.IDUserLogins == _IdUser).AsEnumerable().
                            Select(x =>
                            {
                                x.LoginProvider = TBLogin.Text;
                                x.KeyWord = TBWord.Text;
                                return x;
                            });
                IEnumerable<Users> user = context.Users.Where(x => x.IDUsers == _IdUser).AsEnumerable().
                           Select(x =>
                           {
                               x.Email = TBEmail.Text;
                               x.phone = TBPhone.Text;
                               return x;
                           });
                foreach (UserLogins userLogins1 in userLogins)
                {
                    context.Entry(userLogins1).State = System.Data.Entity.EntityState.Modified;
                }
                foreach (Users _user in user)
                {
                    context.Entry(_user).State = System.Data.Entity.EntityState.Modified;
                }
                SaveChange();
                if (MessageBox.Show("Данные изменены",
                   "Информация", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(NavigationContextMenu))
                        {
                            (window as NavigationContextMenu).FrameContextMenu.Content = new Profile(_IdUser);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
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

        public void InfoLK()
        {
            Download();
            LBFio.Content = Surname + " " + NameUser + " " + Patronymic;
            TBEmail.Text = Email;
            TBPhone.Text = Phone;
            TBLogin.Text = Login;
            TBWord.Text = KeyWord;
        }
        
        public void Download()
        {
            IEnumerable<string> Familia =
                    from Users in context.Users
                    where Users.IDUsers == _IdUser
                    select Users.Surname;
            Surname = Familia.First();

            IEnumerable<string> Imia =
                    from Users in context.Users
                    where Users.IDUsers == _IdUser
                    select Users.Name;
            NameUser = Imia.First();

            IEnumerable<string> Otchesstvo =
                    from Users in context.Users
                    where Users.IDUsers == _IdUser
                    select Users.Patronymic;
            Patronymic = Otchesstvo.First();

            IEnumerable<string> Pochta =
                   from Users in context.Users
                   where Users.IDUsers == _IdUser
                   select Users.Email;
            Email = Pochta.First();

            IEnumerable<string> Telephone =
                   from Users in context.Users
                   where Users.IDUsers == _IdUser
                   select Users.phone;
            Phone = Telephone.First();

            IEnumerable<string> login =
                  from UserLogins in context.UserLogins
                  where UserLogins.IDUsers == _IdUser
                  select UserLogins.LoginProvider;
            Login = login.First();

            IEnumerable<string> word =
                  from UserLogins in context.UserLogins
                  where UserLogins.IDUsers == _IdUser
                  select UserLogins.KeyWord;
            KeyWord = word.First();
        }
    }
}

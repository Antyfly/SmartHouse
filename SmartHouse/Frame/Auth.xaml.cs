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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
        }
        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Recovery recovery = new Recovery();
            Application.Current.MainWindow.Close();
            recovery.ShowDialog();
        }

        private void Entry_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" || Password.Password != "")
            {
                try
                {
                    var datasourse = context.UserLogins.ToList().Where(i => i.LoginProvider == Login.Text && i.ProviderKey == Password.Password)
                        .FirstOrDefault();


                    if (datasourse != null)
                    {
                        var deleted = context.Users.ToList().Where(i => i.IsDelete == 0 && (i.IDUsers == datasourse.IDUsers)).Count();
                        if (deleted != 0)
                        {
                            int ID = datasourse.IDUsers;
                            HomeWindow home = new HomeWindow(ID);
                            Application.Current.MainWindow.Close();
                            home.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь удален! Обратитесь к компании.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль введены неверно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Пустые поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Reg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Registration reg = new Registration();
            Application.Current.MainWindow.Close();
            reg.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
               
        }
    }
}

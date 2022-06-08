using SmartHouse.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using SmartHouse.Frame;
using static SmartHouse.Entity.AppData;
using SmartHouse.Windows;

namespace SmartHouse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
           
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Recovery recovery = new Recovery();
            this.Close();
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
                            var role = context.UserLogins.Where(u => u.IDRole == 1 && (u.IDUsers == datasourse.IDUsers)).Count();
                            if (role != 0)
                            {
                                int ID = datasourse.IDUsers;
                                HomeWindow home = new HomeWindow(ID);
                                this.Close();
                                home.ShowDialog();
                            }else
                            {
                                AdminHome admin = new AdminHome();
                                this.Close();
                                admin.ShowDialog();
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Пользователь удален! Обратитесь к компании.\nНомер для связи: +7(977)499-32-42", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            this.Close();
            reg.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }

}

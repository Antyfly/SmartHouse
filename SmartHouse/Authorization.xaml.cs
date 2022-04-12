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
            try
            {
                var datasourse = context.UserLogins.ToList().Where(i=> i.LoginProvider == Login.Text && i.ProviderKey == Password.Password)
                    .FirstOrDefault();

                if (datasourse != null)
                {
                    //Entity.AppData.context = datasourse;
                    Home home = new Home();
                    this.Close();
                    home.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Логин или пароль введены неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

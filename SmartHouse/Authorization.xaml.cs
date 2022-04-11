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
           // string color = "#FFC8BAD3";
            Home home = new Home();
            this.Close();
            home.ShowDialog();
            
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

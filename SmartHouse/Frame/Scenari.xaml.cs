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
    /// Логика взаимодействия для Scenarios.xaml
    /// </summary>
    public partial class Scenari : Page
    {
        public int _user;
        public Scenari(int IDUsers)
        {
            InitializeComponent();
            _user = IDUsers;
            UpdateList();
        }
        public void UpdateList()
        {
            var CountScenari = context.ScenariosView.Where(i => i.IDUsers == _user).ToList();

            if (CountScenari.Count > 0)
            {
                List.ItemsSource = CountScenari;
            }
        }

        

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
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

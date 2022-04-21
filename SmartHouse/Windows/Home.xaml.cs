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
using System.Windows.Shapes;
using static SmartHouse.Entity.AppData;

namespace SmartHouse
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home(int ID)
        {

            InitializeComponent();
            DataContext = this;
            int _userDefinition = ID;
            Update(_userDefinition);
        }

        private void Lk_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }


        private void ListUpDown_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DarkWindow dark = new DarkWindow();
            dark.Show();
            Add add = new Add();
            add.ShowDialog();
            add.Topmost = false;
            add.WindowState = WindowState.Normal;
            add.Focus();
        }

        private void ExitProfile_Click(object sender, RoutedEventArgs e)
        {
            Authorization auth = new Authorization();
            this.Close();
            auth.ShowDialog();
        }

        private void Profile_click(object sender, RoutedEventArgs e)
        {
            DarkWindow dark = new DarkWindow();
            dark.Show();
            Profile prof = new Profile();
            prof.ShowDialog();
            prof.WindowState = WindowState.Normal;
            prof.Topmost = true;
            prof.Focus();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DarkWindow dark = new DarkWindow();
            dark.Show();
            Add add = new Add();
            add.ShowDialog();
            add.Topmost = false;
            add.WindowState = WindowState.Normal;
            add.Focus();
        }


        private void Update(int ID)
        {
            var datasourse = context.Home.Where(i => i.IDUser == ID).ToList();
            var device = datasourse;
            if (device.Count > 0)
            {
                ListViewer.ItemsSource = device;
            }
             var selection = device.Select(x => x.NameRoom).Distinct().ToList();
            selection.Insert(0, "Все устройства");
            cbRoom.ItemsSource = selection;

            
        }
    }
}

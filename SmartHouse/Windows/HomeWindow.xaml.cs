using SmartHouse.Entity;
using System;
using System.Collections.Generic;
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
using static SmartHouse.Entity.AppData;

namespace SmartHouse
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        
        public int _userDefinition;
        public int IdentiDevice;
        public HomeWindow(int ID)
        { 
            InitializeComponent();
            DataContext = this;
            _userDefinition = ID;
            TxtInfo.Visibility = Visibility.Visible;
            Info.Visibility = Visibility.Hidden;
            UpdateList();

        }

        #region выпадающий список личного кабмнета 
        private void Lk_Click(object sender, RoutedEventArgs e)
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

        private void ExitProfile_Click(object sender, RoutedEventArgs e)
        {
            Authorization auth = new Authorization();
            this.Close();
            auth.ShowDialog();
        }

        #endregion

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        #region выведение и клик Listview
        public void UpdateList()
        {
            int ID = _userDefinition;

            var datasourse = context.Home.Where(i => i.IDUser == ID).Select(x => x.NameRoom).Distinct().ToList();
            datasourse.Insert(0, "Все устройства");
            cbRoom.ItemsSource = datasourse;
            cbRoom.SelectedIndex = 0;

            var selectionproba = context.Home.Where(i => i.IDUser == ID).ToList();

            if (selectionproba.Count > 0 && cbRoom.SelectedIndex == 0)
            {
                ListViewer.ItemsSource = selectionproba;
            }
            

        }

        private void ListViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListViewer.SelectedItem is Home InfoDevice)
            {
                Information(InfoDevice);
                IdentiDevice = InfoDevice.IDOurControllers;

            }
        }

        #endregion

        #region выпадающий список девайсов
        private void cbRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ID = _userDefinition;
            var selectedcomboItem = sender as ComboBox;
            string name = selectedcomboItem.SelectedItem as string ;
            if (cbRoom.SelectedIndex != 0)
            {
                var selection = context.Home.Where(i => i.IDUser == ID && i.NameRoom == name).ToList();
                ListViewer.ItemsSource = selection;
            }
            else
            {
                var selectionproba = context.Home.Where(i => i.IDUser == ID).ToList();
                ListViewer.ItemsSource = selectionproba;
                
            }
   
        }
        #endregion


        #region информация девайсов 
        public void Information(Home InfoDevice)
        {
            TxtInfo.Visibility = Visibility.Hidden;
            Info.Visibility = Visibility.Visible;

            NameDevice.Text = InfoDevice.Title;

            if (InfoDevice.Condition == true)
            {
                PowerON.Visibility = Visibility.Visible;
                PowerOFF.Visibility = Visibility.Hidden;
            }
            else
            {
                PowerOFF.Visibility = Visibility.Visible;
                PowerON.Visibility = Visibility.Hidden;
            }

            
        }
        private void PowerOFF_Click(object sender, RoutedEventArgs e)
        {
            saveOFFButton();
            PowerON.Visibility = Visibility.Visible;
            PowerOFF.Visibility = Visibility.Hidden;
            UpdateList();
        }

        private void PowerON_Click(object sender, RoutedEventArgs e)
        {
            saveOnButton();
            PowerON.Visibility = Visibility.Hidden;
            PowerOFF.Visibility = Visibility.Visible;
            UpdateList();
        }


        public void saveOnButton()
        {
            IEnumerable<OurControllers> home = context.OurControllers.Where(x => x.IDOurControllers == IdentiDevice).AsEnumerable().
                           Select(x =>
                           {
                               x.Condition = false;
                               return x;
                           });

            foreach (OurControllers home1 in home)
            {
                context.Entry(home1).State = System.Data.Entity.EntityState.Modified;
            }
            try
            {
                context.SaveChanges();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void saveOFFButton()
        {
            IEnumerable<OurControllers> home = context.OurControllers.Where(x => x.IDOurControllers == IdentiDevice).AsEnumerable().
                           Select(x =>
                           {
                               x.Condition = true;
                               return x;
                           });

            foreach (OurControllers home1 in home)
            {
                context.Entry(home1).State = System.Data.Entity.EntityState.Modified;
            }
            try
            {
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}

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


namespace SmartHouse
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {

        public int _userDefinition;
        public int IdentiDevice;
        public int idFlat;
        public HomeWindow(int ID)
        {
            InitializeComponent();
            DataContext = this;
            _userDefinition = ID;
            TxtInfo.Visibility = Visibility.Visible;
            Info.Visibility = Visibility.Hidden;
            ComboFlat();
            ComboRoom();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        // НЕ ТРОГАТЬ!!! ОСОБЕННО КОСТЫЛИ
        #region выпадающий список личного кабинета 
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
            string name = "Добавление";
            NavigationContextMenu add = new NavigationContextMenu(name, _userDefinition);
            add.ShowDialog();
            add.Topmost = false;
            add.WindowState = WindowState.Normal;
            add.Focus();
        }

        private void Profile_click(object sender, RoutedEventArgs e)
        {
            DarkWindow dark = new DarkWindow();
            dark.Show();
            string name = "Профиль";
            NavigationContextMenu prof = new NavigationContextMenu(name, _userDefinition);
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
        // НЕ ТРОГАТЬ!!! ОСОБЕННО КОСТЫЛИ
        #region выведение и клик Listview
        public void ComboFlat()
        {
            var datasourse = context.HomeDop.Where(i => i.IDUsers == _userDefinition && i.FlatName == CBHouse.Text).Select(x => x.NameRoom).Distinct().ToList();
            datasourse.Insert(0, "Все устройства");
            CbRoom.ItemsSource = datasourse;
            CbRoom.SelectedIndex = 0;


            //вывод в выпадающий список квартиры
            var sourse = context.Flat.Where(i => i.IDUser == _userDefinition).Select(x => x.FlatName).Distinct().ToList();
            if (sourse.Count == 0)
            {
                sourse.Insert(0, "Нет квартир");
                CBHouse.SelectedIndex = 0;
            }
            else
            {
                CBHouse.ItemsSource = sourse;
                CBHouse.SelectedIndex = 0;
            }
        }

        public void ComboRoom()
        {
            IEnumerable<int> flat =
                    from Flat in context.Flat
                    where Flat.FlatName == CBHouse.SelectedItem.ToString()
                    select Flat.IDFlat;
            idFlat = flat.First();

            //вывод в выпадающий список комнаты
            var datasourse = context.HomeDop.Where(i => i.IDUsers == _userDefinition && i.IDFlat == idFlat).Select(x => x.NameRoom).Distinct().ToList();
            datasourse.Insert(0, "Все устройства");
            CbRoom.ItemsSource = datasourse;
            CbRoom.SelectedIndex = 0;
        }
        private void ListViewer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameDevice = CbRoom.Text;

            var textHouse = CBHouse.Text;
            if (CbRoom.SelectedIndex != 0)
            {
                var selection = context.Home.Where(i => i.IDUser == _userDefinition && i.NameRoom == nameDevice && i.FlatName == CBHouse.SelectedItem.ToString()).ToList();
                ListViewer.ItemsSource = selection;
                List();
            }
            else
            {
                var selectionproba = context.Home.Where(i => i.IDUser == _userDefinition && i.FlatName == textHouse).ToList();
                ListViewer.ItemsSource = selectionproba;
            }
        }

        public void List()
        {
            var CountFlat = context.Home.Where(i => i.IDUser == _userDefinition && i.FlatName == CBHouse.SelectedItem.ToString()).ToList();

            if (CountFlat.Count > 0 && CbRoom.SelectedIndex == 0)
            {
                ListViewer.ItemsSource = CountFlat;
            }
            else if (CountFlat.Count == 0)
            {
                var Flat = context.Home.Where(i => i.IDUser == _userDefinition).ToList();
                ListViewer.ItemsSource = Flat;
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
        // НЕ ТРОГАТЬ!!! ОСОБЕННО КОСТЫЛИ
        #region выпадающий список девайсов и домов
        private void CbRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedcomboItem = sender as ComboBox;
            string nameDevice = selectedcomboItem.SelectedItem as string;

            var textHouse = CBHouse.Text;
            if (CbRoom.SelectedIndex != 0)
            {
                var selection = context.Home.Where(i => i.IDUser == _userDefinition && i.NameRoom == nameDevice && i.FlatName == CBHouse.SelectedItem.ToString()).ToList();
                ListViewer.ItemsSource = selection;
                List();
            }
            else
            {
                var selectionproba = context.Home.Where(i => i.IDUser == _userDefinition && i.FlatName == textHouse).ToList();
                ListViewer.ItemsSource = selectionproba;
            }

        }

        private void CBHouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedHouseItem = sender as ComboBox;
            string name = selectedHouseItem.SelectedItem as string;
            if (CbRoom.SelectedIndex != 0)
            {
                var selection = context.Home.Where(i => i.IDUser == _userDefinition && i.FlatName == name && i.NameRoom == CbRoom.SelectedItem.ToString()).ToList();
                ListViewer.ItemsSource = selection;
                ComboRoom();
            }
            else
            {
                var selectionproba = context.Home.Where(i => i.IDUser == _userDefinition && i.FlatName == name).ToList();
                ListViewer.ItemsSource = selectionproba;
                ComboRoom();
            }

        }
        #endregion
        // НЕ ТРОГАТЬ!!! ОСОБЕННО КОСТЫЛИ
        #region информация девайсов 
        public void Information(Home InfoDevice)
        {
            TxtInfo.Visibility = Visibility.Hidden;
            Info.Visibility = Visibility.Visible;
            ConditionPowerConsumption.Text = InfoDevice.PowerConsumption.ToString() + " Вт";
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
            if (InfoDevice.Condition == true)
            {
                Condition.Text = "Включено";
            }
            if (InfoDevice.Condition == false)
            {
                Condition.Text = "Выключено";
            }

        }
        private void PowerOFF_Click(object sender, RoutedEventArgs e)
        {
            SaveOFFButton();
            PowerON.Visibility = Visibility.Visible;
            PowerOFF.Visibility = Visibility.Hidden;
            Condition.Text = "Включено";
        }

        private void PowerON_Click(object sender, RoutedEventArgs e)
        {
            SaveOnButton();
            PowerON.Visibility = Visibility.Hidden;
            PowerOFF.Visibility = Visibility.Visible;
            Condition.Text = "Выключено";
        }


        public void SaveOnButton()
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
        public void SaveOFFButton()
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

        #region удаление устройств
        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы точно уверены?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                if (ListViewer.SelectedItem is Home InfoDevice)
                {
                    context.OurControllers.Remove(context.OurControllers.Where(i => i.IDOurControllers == InfoDevice.IDOurControllers).FirstOrDefault());
                    context.SaveChanges();
                    List();
                }
            }
        }
        #endregion

    }
}

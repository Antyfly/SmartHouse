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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public int _userDefinition;
        public Add( int IDUsers)
        {
            _userDefinition = IDUsers;
            InitializeComponent();
            Update();
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

        public void Update()
        {
            
        }

        private void AddComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddComboBox.SelectedIndex == 0)
            {
                DeviceGroupBox.Visibility = Visibility.Visible;
                RoomsGroupBox.Visibility = Visibility.Visible;
                TimeGroupBox.Visibility = Visibility.Hidden;
                NameHouseGroupBox.Visibility = Visibility.Hidden;
                CityGroupBox.Visibility = Visibility.Hidden;
                StreetGroupBox.Visibility = Visibility.Hidden;
                NumberHouseGroupBox.Visibility = Visibility.Hidden;
                DayWeekGroupBox.Visibility = Visibility.Hidden;
                SaveButton.Visibility = Visibility.Visible;
                NameRoomsGroupBox.Visibility = Visibility.Hidden;
                HouseGroupBox.Visibility = Visibility.Hidden;
                var datasourse = context.Home.Where(i => i.IDUser == _userDefinition).Select(x => x.NameRoom).Distinct().ToList();
                RoomsComboBox.ItemsSource = datasourse;
            }
            else if (AddComboBox.SelectedIndex == 1)
            {
                DeviceGroupBox.Visibility = Visibility.Visible;
                TimeGroupBox.Visibility = Visibility.Visible;
                RoomsGroupBox.Visibility = Visibility.Hidden;
                NameHouseGroupBox.Visibility =  Visibility.Hidden;
                CityGroupBox.Visibility = Visibility.Hidden;
                StreetGroupBox.Visibility = Visibility.Hidden;
                NumberHouseGroupBox.Visibility = Visibility.Hidden;
                DayWeekGroupBox.Visibility= Visibility.Visible;
                SaveButton.Visibility = Visibility.Visible;
                NameRoomsGroupBox.Visibility = Visibility.Hidden;
                HouseGroupBox.Visibility = Visibility.Hidden;
            }
            else if (AddComboBox.SelectedIndex == 2)
            {
                DeviceGroupBox.Visibility = Visibility.Hidden;
                TimeGroupBox.Visibility = Visibility.Hidden;
                RoomsGroupBox.Visibility = Visibility.Hidden;
                NameHouseGroupBox.Visibility = Visibility.Visible;
                CityGroupBox.Visibility = Visibility.Visible;
                StreetGroupBox.Visibility = Visibility.Visible;
                NumberHouseGroupBox.Visibility = Visibility.Visible;
                DayWeekGroupBox.Visibility = Visibility.Hidden;
                SaveButton.Visibility = Visibility.Visible;
                NameRoomsGroupBox.Visibility = Visibility.Hidden;
                HouseGroupBox.Visibility = Visibility.Hidden;
            }
            else if(AddComboBox.SelectedIndex == 3)
            {
                DeviceGroupBox.Visibility = Visibility.Hidden;
                TimeGroupBox.Visibility = Visibility.Hidden;
                RoomsGroupBox.Visibility = Visibility.Hidden;
                NameHouseGroupBox.Visibility = Visibility.Hidden;
                CityGroupBox.Visibility = Visibility.Hidden;
                StreetGroupBox.Visibility = Visibility.Hidden;
                NumberHouseGroupBox.Visibility = Visibility.Hidden;
                DayWeekGroupBox.Visibility = Visibility.Hidden;
                SaveButton.Visibility = Visibility.Visible;
                NameRoomsGroupBox.Visibility = Visibility.Visible;
                HouseGroupBox.Visibility = Visibility.Visible;
                var sourse = context.Home.Where(i => i.IDUser == _userDefinition).Select(x => x.FlatName).Distinct().ToList();
                HouseComboBox.ItemsSource = sourse;
            }
        }

       
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

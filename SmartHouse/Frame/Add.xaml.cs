using SmartHouse.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
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
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window1 in Application.Current.Windows)
            {
                if (window1.GetType() == typeof(NavigationContextMenu))
                {
                    (window1 as NavigationContextMenu).Close();
                }
            }
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(DarkWindow))
                {
                    (window as DarkWindow).Close();
                }
            }
        }

        private void AddComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var datasourse = context.Home.Where(i => i.IDUser == _userDefinition).Select(x => x.NameRoom).Distinct().ToList();
            RoomsComboBox.ItemsSource = datasourse;

            var sourse = context.Flat.Where(i => i.IDUser == _userDefinition).Select(x => x.FlatName).Distinct().ToList();
            HouseComboBox.ItemsSource = sourse;

            if (AddComboBox.SelectedIndex == 0)
            {
                DeviceGroupBox.Visibility = Visibility.Visible;
                RoomsGroupBox.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Visible;
                HouseGroupBox.Visibility = Visibility.Visible;


                TimeGroupBox.Visibility = Visibility.Hidden;
                NameHouseGroupBox.Visibility = Visibility.Hidden;
                CityGroupBox.Visibility = Visibility.Hidden;
                StreetGroupBox.Visibility = Visibility.Hidden;
                NumberHouseGroupBox.Visibility = Visibility.Hidden;
                DayWeekGroupBox.Visibility = Visibility.Hidden;
                NameRoomsGroupBox.Visibility = Visibility.Hidden;
            }
            else if (AddComboBox.SelectedIndex == 1)
            {
                DeviceGroupBox.Visibility = Visibility.Visible;
                TimeGroupBox.Visibility = Visibility.Visible;
                DayWeekGroupBox.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Visible;

                RoomsGroupBox.Visibility = Visibility.Hidden;
                NameHouseGroupBox.Visibility =  Visibility.Hidden;
                CityGroupBox.Visibility = Visibility.Hidden;
                StreetGroupBox.Visibility = Visibility.Hidden;
                NumberHouseGroupBox.Visibility = Visibility.Hidden;
                NameRoomsGroupBox.Visibility = Visibility.Hidden;
                HouseGroupBox.Visibility = Visibility.Hidden;
            }
            else if (AddComboBox.SelectedIndex == 2)
            {
                NameHouseGroupBox.Visibility = Visibility.Visible;
                CityGroupBox.Visibility = Visibility.Visible;
                StreetGroupBox.Visibility = Visibility.Visible;
                NumberHouseGroupBox.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Visible;
                SizeGroupBox.Visibility = Visibility.Visible;

                DeviceGroupBox.Visibility = Visibility.Hidden;
                TimeGroupBox.Visibility = Visibility.Hidden;
                RoomsGroupBox.Visibility = Visibility.Hidden;
                DayWeekGroupBox.Visibility = Visibility.Hidden;
                NameRoomsGroupBox.Visibility = Visibility.Hidden;
                HouseGroupBox.Visibility = Visibility.Hidden;
            }
            else if(AddComboBox.SelectedIndex == 3)
            {
                SaveButton.Visibility = Visibility.Visible;
                NameRoomsGroupBox.Visibility = Visibility.Visible;
                HouseGroupBox.Visibility = Visibility.Visible;
                WidthGroupBox.Visibility = Visibility.Visible;
                HeightGroupBox.Visibility = Visibility.Visible;

                DeviceGroupBox.Visibility = Visibility.Hidden;
                TimeGroupBox.Visibility = Visibility.Hidden;
                RoomsGroupBox.Visibility = Visibility.Hidden;
                NameHouseGroupBox.Visibility = Visibility.Hidden;
                CityGroupBox.Visibility = Visibility.Hidden;
                StreetGroupBox.Visibility = Visibility.Hidden;
                NumberHouseGroupBox.Visibility = Visibility.Hidden;
                DayWeekGroupBox.Visibility = Visibility.Hidden;
            }
        }

       
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddComboBox.SelectedIndex == 0)
            {
                IEnumerable<int> flat =
                    from Flat in context.Flat
                    where Flat.FlatName == HouseComboBox.Text
                    select Flat.IDFlat;
                int idFlat = flat.FirstOrDefault();

                
                try
                {
                    context.OurControllers.Add(new OurControllers
                    {
                        IDRoom = context.Rooms.Where(d => d.NameRoom == RoomsComboBox.Text && d.IDFlat == idFlat).Select(d => d.IDRooms).FirstOrDefault(),
                        IDDevice = context.Device.Where(d => d.Title == DeviceComboBox.Text).Select(d => d.IDDevice).FirstOrDefault(),
                        Condition = false,
                        PowerConsumption = 0

                    });

                    context.SaveChanges();
                    MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (AddComboBox.SelectedIndex == 1)
            {
                try
                {
                    context.Scenarios.Add(new Scenarios
                    {
                        IDDevice = context.Device.Where(d => d.Title == DeviceComboBox.Text).Select(d => d.IDDevice).FirstOrDefault(),
                        Time = TimeSpan.Parse(TimeScenarios.Text),
                        Action = true,
                        DayWeek = DayWeekComboBox.SelectedIndex,
                    });
                    context.SaveChanges();
                    MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (AddComboBox.SelectedIndex == 2)
            {
                try
                {

                    context.Flat.Add(new Flat
                    {
                        FlatName = NameHouseTextBox.Text,
                        IDUser = _userDefinition,
                        Size = float.Parse(SizeTextBox.Text),
                    });

                    context.Address.Add(new Address
                    {
                        City = CityTextBox.Text,
                        Street = StreetTextBox.Text,
                        NumberHouse = NumberHouseTextBox.Text,
                    });
                    context.SaveChanges();
                    MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else if (AddComboBox.SelectedIndex == 3)
            {
                try
                {
                    IEnumerable<int> flat =
                    from Flat in context.Flat
                    where Flat.FlatName == HouseComboBox.Text
                    select Flat.IDFlat;
                    int idFlat = flat.FirstOrDefault();

                    context.Rooms.Add(new Rooms
                    {
                        IDFlat = context.Flat.Where(f => f.IDFlat == idFlat).Select(d => d.IDFlat).FirstOrDefault(),
                        NameRoom = NameRoomsTextBox.Text,
                        HeightRoom = float.Parse(HeightTextBox.Text),
                        WidthRoom = float.Parse(WidthTextBox.Text),
                    });
                    context.SaveChanges();
                    MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

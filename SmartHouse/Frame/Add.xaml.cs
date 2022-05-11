using SmartHouse.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

            var sourse = context.Home.Where(i => i.IDUser == _userDefinition).Select(x => x.FlatName).Distinct().ToList();
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
                //int proba = context.Rooms.Where(d => d.NameRoom == RoomsComboBox.Text).Select(d => d.IDRooms).FirstOrDefault();
                //qwerty.Content = RoomsComboBox.Text;

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
                    MessageBox.Show("Данные изменены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
           
        }
    }
}

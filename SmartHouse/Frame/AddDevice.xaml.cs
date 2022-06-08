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
using System.IO;
using Microsoft.Win32;
using static SmartHouse.Entity.AppData;
using SmartHouse.Entity;

namespace SmartHouse.Frame
{
    /// <summary>
    /// Логика взаимодействия для AddDevice.xaml
    /// </summary>
    public partial class AddDevice : Page
    {
        private string DeviceImagePath;

        private BitmapImage ChangedPhoto = null;
        public AddDevice()
        {
            InitializeComponent();
            TipeConnectionComboBox.ItemsSource = context.TypeConnection.Select(tc => tc.Name).ToList();
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ErrorMessage = "";

            if (TitleTextBox.Text.Equals(string.Empty))
                ErrorMessage += "Не введено название устройства! \n";

            if (DeviceImagePath is null)
                ErrorMessage += "Не выбрано фото! \n";

            if (ErrorMessage.Length != 0)
            {
                MessageBox.Show("Ошибка: \n" + ErrorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                try
                {
                    context.Device.Add(new Device
                    {
                        Title = TitleTextBox.Text,
                        IDTypeConnection = TipeConnectionComboBox.SelectedIndex + 1,
                        ImagePath = DeviceImagePath,
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

        private void ImageButon_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image (*.png);(*.jpg);(*.jpeg) | *.png;*.jpg;*.jpeg;";


            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                File.Copy(path, Directory.GetCurrentDirectory() + $@"\Device\{System.IO.Path.GetFileName(path)}", true);
                ChangedPhoto = new BitmapImage(new Uri(path, UriKind.Absolute));
                DeviceImage.Source = ChangedPhoto;


                DeviceImagePath = $@"\Device\" + System.IO.Path.GetFileName(path);
            }
        }

        private void TipeConnectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            string selection = context.TypeConnection.Where(i => i.Name == TipeConnectionComboBox.SelectedItem.ToString()).Select(tp=> tp.Description).FirstOrDefault();
            InfoTipeConnectionTextBlock.Text = selection;
        }
    }
}


using System.Windows;
using SmartHouse.Frame;

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
            AuthFrame.Content = new Auth();
        }
    }

}

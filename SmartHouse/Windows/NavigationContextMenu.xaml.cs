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
using SmartHouse.Frame;

namespace SmartHouse
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class NavigationContextMenu : Window
    {
        public string nameCM;
        public int IDUsers;
        public NavigationContextMenu(string name, int ID)
        {
            InitializeComponent();
            nameCM = name;
            IDUsers = ID;
            this.Topmost = true;
            FrameNavigation();
            
        }
        public void FrameNavigation()
        {
            if(nameCM == "Профиль")
            {
                FrameContextMenu.Content = new Profile(IDUsers);
            }
            else if (nameCM == "Сценарии")
            {
                FrameContextMenu.Content = new Scenari(IDUsers);
            }
            else if (nameCM == "Добавление")
            {
                FrameContextMenu.Content = new Add(IDUsers);
            }
        }
        
    }
}

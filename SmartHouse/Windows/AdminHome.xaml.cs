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

namespace SmartHouse.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Window
    {
        public AdminHome()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {

            var datasourse = context.userView.ToList();
            ListViews.ItemsSource = datasourse;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Recovery_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно уверены?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                if (ListViews.SelectedItem is userView recover)
                {
                    var datasourse = context.UserLogins.ToList();
                    IEnumerable<int> IDUser =
                   from UserLogins in context.UserLogins
                   where UserLogins.IDUserLogins == recover.IDUserLogins
                   select UserLogins.IDUsers;
                    int ID = IDUser.First();

                    IEnumerable<Users> user = context.Users.Where(x => x.IDUsers == ID).AsEnumerable().
                            Select(x =>
                            {
                                x.Surname = recover.Surname;
                                x.Name = recover.Name;
                                x.Patronymic = recover.Patronymic;
                                x.phone = recover.phone;
                                x.IsDelete = 0;
                                return x;
                            });

                    foreach (Users user1 in user)
                    {
                        context.Entry(user1).State = System.Data.Entity.EntityState.Modified;
                    }
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Успешно восстановлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void PoiskFamily_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PoiskName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PoiskEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

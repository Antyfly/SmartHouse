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
    /// Логика взаимодействия для Recovery.xaml
    /// </summary>
    public partial class Recovery : Window
    {
        public Recovery()
        {
            InitializeComponent();
            
        }

        private void Entry_Click(object sender, RoutedEventArgs e)
        {
            if (Email.Text != "" && CodeWord.Text != "")
            {
                try
                {
                   var datasourse = context.UserLogins.Where(i => i.LoginProvider == Email.Text && i.KeyWord == CodeWord.Text)
                    .Count();

                    if (datasourse != 0)
                    {
                        LoginCode.Visibility = Visibility.Hidden;
                        Password.Visibility = Visibility.Visible; 
                    }
                    else
                    {
                        MessageBox.Show("Логин или кодовое слово введены неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
            else
            {
                MessageBox.Show("Пустые поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Recover_Click(object sender, RoutedEventArgs e)
        {
            if (PassText.Password != "" && RepeatText.Password != "" && PassText.Password == RepeatText.Password)
            { 
                if (MessageBox.Show("Вы точно уверены?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                   
                        var datasourse = context.UserLogins.ToList();
                        IEnumerable<int> IDUser =
                       from UserLogins in context.UserLogins
                       where UserLogins.LoginProvider == Email.Text
                       select UserLogins.IDUserLogins;
                        int ID = IDUser.First();


                        IEnumerable<UserLogins> userLogins = context.UserLogins.Where(x => x.IDUserLogins == ID).AsEnumerable().
                            Select(x =>
                            {
                                x.LoginProvider = Email.Text;
                                x.ProviderKey = PassText.Password.ToString();
                                x.KeyWord = CodeWord.Text;
                                return x;
                            });
                    foreach (UserLogins userLogins1 in userLogins)
                    {
                        context.Entry(userLogins1).State = System.Data.Entity.EntityState.Modified;
                    }
                    try
                    {
                        context.SaveChanges();
                        if (MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        {
                            Authorization auth = new Authorization();
                            this.Close();
                            auth.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (PassText.Password != RepeatText.Password)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Пустые поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Authorization auth = new Authorization();
            this.Close();
            auth.ShowDialog();
        }
    }
}

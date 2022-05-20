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
using Xceed.Wpf.Toolkit;
using static SmartHouse.Entity.AppData;
using static SmartHouse.Help.Validation;

namespace SmartHouse
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }


        private void BtnRegistr_Click(object sender, RoutedEventArgs e)
        {
            string logBD = context.UserLogins.Where(i => i.LoginProvider == Email.Text).Select(j => j.LoginProvider).FirstOrDefault();

            if (Name.Text == "" || Patronymic.Text == "" || Surname.Text == "" || Email.Text == "" || phone.Text == "" || PassText.Password == "" || Codeword.Text == "")
            {
                System.Windows.MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (logBD != null)
            { 
                System.Windows.MessageBox.Show("Логин уже используется", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidatePassw(PassText.Password.ToString()) == false)
            {
                System.Windows.MessageBox.Show("Недопустимый пароль, необходимо ввести (от 8 символов, минимум одна: заглавная, строчная, спец.символ)", "Регистрация пользователя", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (ValidateEmail(Email.Text) == false)
            {
                System.Windows.MessageBox.Show("Неверная почта, проверьте написание", "Регистрация пользователя", MessageBoxButton.OK, MessageBoxImage.Exclamation); 
            }
            else
            {
                try
                {
                    context.UserLogins.Add(new UserLogins
                    {
                        LoginProvider = Email.Text.ToString(),
                        ProviderKey = PassText.Password.ToString(),
                        KeyWord = Codeword.Text.ToString(),
                        IDRole = 1
                    });
                    context.Users.Add(new Users
                    {
                        Surname = Surname.Text.ToString(),
                        Name = Name.Text.ToString(),
                        Patronymic = Patronymic.Text.ToString(),
                        Email = Email.Text.ToString(),
                        phone = phone.Text.ToString(),
                        IsDelete = 0
                    });
                    context.SaveChanges();
                    if (System.Windows.MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                    {
                        Authorization auth = new Authorization();
                        this.Close();
                        auth.ShowDialog();
                    }
                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.Message);
                }
                
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

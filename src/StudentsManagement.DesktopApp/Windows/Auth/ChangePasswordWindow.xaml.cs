using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Helpers;
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

namespace StudentsManagement.DesktopApp.Windows.Auth
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private readonly IUsersService _usersService;
        private readonly Guid _currentUserId;

        public ChangePasswordWindow(Guid currentUserId, IUsersService usersService)
        {
            InitializeComponent();

            _usersService = usersService;
            _currentUserId = currentUserId;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(OldPasswordInput.Password))
            {
                MessageBox.Show("Необходимо ввести старый пароль!");
                return;
            }

            if(string.IsNullOrEmpty(NewPasswordInput.Password))
            {
                MessageBox.Show("Необходимо ввести новый пароль!");
                return;
            }

            if(string.IsNullOrEmpty(NewPasswordRepeatInput.Password))
            {
                MessageBox.Show("Необходимо ввести повтор нового пароля!");
                return;
            }

            var currentUser = _usersService.GetById(_currentUserId);

            if(currentUser.PasswordHash != AuthHelper.CreateSha256Hash(OldPasswordInput.Password))
            {
                MessageBox.Show("Старый пароль неверен!");
                return;
            }

            if(NewPasswordInput.Password != NewPasswordRepeatInput.Password)
            {
                MessageBox.Show("Новый пароль и его повтор не совпадают!");
                return;
            }

            currentUser.PasswordHash = AuthHelper.CreateSha256Hash(NewPasswordInput.Password);
            _usersService.Update(currentUser);

            MessageBox.Show("Пароль успешно изменён!");

            this.Close();
        }
    }
}

using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Helpers;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAuthService _authService;

        public event CustomEventHandler OnSuccess;

        public LoginWindow(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;

            if (string.IsNullOrEmpty(EmailInput.Text))
            {
                MessageBox.Show(AppLocalization.NotFilledInMessageText + "\"Email\"");
                LoginButton.IsEnabled = true;
            }

            if(string.IsNullOrEmpty(PasswordInput.Password))
            {
                MessageBox.Show(AppLocalization.NotFilledInMessageText + "\"Пароль\"");
                LoginButton.IsEnabled = true;
            }

            try
            {
                var result = _authService.SignIn(
                    EmailInput.Text,
                    AuthHelper.CreateSha256Hash(PasswordInput.Password));

                OnSuccess?.Invoke(this, new CustomEventArgs(result, ));

                this.Close();
            }
            catch(Microsoft.Data.SqlClient.SqlException ex)
            {
                LoginButton.IsEnabled = true;

                MessageBox.Show(ex.Message, AppLocalization.DatabaseExceptionTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(BusinessLogicException ex)
            {
                LoginButton.IsEnabled = true;

                MessageBox.Show(ex.Message);
            }
        }

        private void LoginAsAdmin_Click(object sender, RoutedEventArgs e)
        {
            // TODO REMOVE THIS WINDOW
            EmailInput.Text = "admin@ya.ru";
            PasswordInput.Password = "123456_Aa";

            LoginButton_Click(sender, e);
        }
    }
}

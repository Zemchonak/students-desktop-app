using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Helpers;
using System;
using System.Windows;

namespace StudentsManagement.DesktopApp.AuthWindows
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

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;

            if (string.IsNullOrEmpty(EmailInput.Text))
            {
                MessageBox.Show(Localization.NotFilledInMessageText + "\"Email\"");
                LoginButton.IsEnabled = true;
            }

            if(string.IsNullOrEmpty(PasswordInput.Password))
            {
                MessageBox.Show(Localization.NotFilledInMessageText + "\"Password\"");
                LoginButton.IsEnabled = true;
            }

            try
            {
                //var result = await _authService.SignIn(
                //    EmailInput.Text, 
                //    AuthHelper.CreateSha256Hash(PasswordInput.Password));

                var result = Guid.NewGuid().ToString();
                OnSuccess?.Invoke(this, new CustomEventArgs { Id = result });

                this.Close();
            }
            catch(Microsoft.Data.SqlClient.SqlException ex)
            {
                LoginButton.IsEnabled = true;

                MessageBox.Show(ex.Message, Localization.DatabaseExceptionTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(BusinessLogicException ex)
            {
                LoginButton.IsEnabled = true;

                MessageBox.Show(ex.Message);
            }
        }
    }
}

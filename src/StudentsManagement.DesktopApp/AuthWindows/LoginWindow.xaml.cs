using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Helpers;
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
            if(string.IsNullOrEmpty(EmailInput.Text))
            {
                MessageBox.Show(Localization.NotFilledInMessageText + "\"Email\"");
            }

            if(string.IsNullOrEmpty(PasswordInput.Password))
            {
                MessageBox.Show(Localization.NotFilledInMessageText + "\"Password\"");
            }

            try
            {
                var result = await _authService.SignIn(
                    EmailInput.Text, 
                    AuthHelper.CreateSha256Hash(PasswordInput.Password));

                OnSuccess?.Invoke(this, new CustomEventArgs { Id = result });

                this.Close();
            }
            catch(BusinessLogicException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

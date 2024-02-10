using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Enums;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.AuthWindows;
using StudentsManagement.DesktopApp.EventHandlers;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentsManagement.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CurrentUserId;

        private readonly IAuthService _authService;
        private readonly IUsersService _usersService;

        public MainWindow(IAuthService authService, IUsersService usersService)
        {
            InitializeComponent();
            ProfileButton.Content = Localization.LoginButtonText;

            _authService = authService;
            _usersService = usersService;
            CurrentUserId = null;
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentUserId == null)
            {
                var loginWindow = new LoginWindow(_authService);
                loginWindow.OnSuccess += HandleSuccessfulLogin;
                loginWindow.Show();
            }
            else
            {
                // Profile window
                MessageBox.Show("PROFILE WINDOW!");
            }
        }

        private async void HandleSuccessfulLogin(object sender, CustomEventArgs e)
        {
            ProfileButton.Content = Localization.ProfileButtonText;

            CurrentUserId = e.Id;

            var user = await _usersService.GetById(CurrentUserId);

            // получить ФИО пользователя
            var userName = new StringBuilder($"{user.FirstName} ");
            if(!string.IsNullOrEmpty(user.MiddleName))
            {
                userName.Append($"{user.MiddleName[0]}.");
            }
            userName.Append($"{user.LastName[0]}.");

            CurrentUser.Text = Localization.SignedInAsText + userName.ToString();

            MessageBox.Show(Localization.WelcomeMessageText + userName.ToString());
        }
    }
}

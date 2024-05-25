using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Utils;
using StudentsManagement.DesktopApp.Windows;
using StudentsManagement.DesktopApp.Windows.Auth;
using StudentsManagement.DesktopApp.Windows.Profile;
using System;
using System.Text;
using System.Windows;

namespace StudentsManagement.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Guid? CurrentUserId;

        private readonly IAuthService _authService;
        private readonly IUsersService _usersService;
        private readonly IFacultiesService _facultiesService;
        private readonly ISpecialitiesService _specialitiesService;
        private readonly IDisciplinesService _disciplinesService;
        private readonly ICurriculumUnitsService _curriculumUnitsService;
        private readonly IGroupsService _groupsService;
        private readonly IAttestationsService _attestationsService;
        private readonly IMarksService _marksService;
        private readonly IRetakeResultsService _retakeResultsService;

        public MainWindow(
            IAuthService authService,
            IUsersService usersService,
            IFacultiesService facultiesService,
            ISpecialitiesService specialitiesService,
            IDisciplinesService disciplinesService,
            ICurriculumUnitsService curriculumUnitsService,
            IGroupsService groupsService,
            IAttestationsService attestationsService,
            IMarksService marksService,
            IRetakeResultsService retakeResultsService)
        {
            InitializeComponent();
            ProfileButton.Content = AppLocalization.LoginButtonText;

            _authService = authService;
            _usersService = usersService;
            _facultiesService = facultiesService;
            _specialitiesService = specialitiesService;
            _disciplinesService = disciplinesService;
            _curriculumUnitsService = curriculumUnitsService;
            _groupsService = groupsService;
            _attestationsService = attestationsService;
            _marksService = marksService;
            _retakeResultsService = retakeResultsService;

            CurrentUserId = null;

            FillSpecialitiesComboBox();
    }

    private void FillSpecialitiesComboBox()
    {
        /*
         TODO

           Load faculties, specitialities, groups

            Fill SpecialitiesComboBox with options
         */
    }

    private void ProfileButton_Click(object sender, RoutedEventArgs e)
    {
        if (CurrentUserId == null)
        {
            var loginWindow = new LoginWindow(_authService);
            loginWindow.OnSuccess += HandleSuccessfulLogin;
            loginWindow.Show();
        }
        else
        {
            var profileWindow = new ProfileWindow(CurrentUserId.Value, _facultiesService, _specialitiesService);
            profileWindow.Show();
        }
    }

    private void HandleSuccessfulLogin(object sender, CustomEventArgs e)
    {
        ProfileButton.Content = AppLocalization.ProfileButtonText;

        CurrentUserId = e.Id;

        var user = _usersService.GetById(CurrentUserId.Value);

        // получить ФИО пользователя
        var userName = new StringBuilder($"{user.FirstName} ");
        if (!string.IsNullOrEmpty(user.MiddleName))
        {
            userName.Append($"{user.MiddleName[0]}.");
        }
        userName.Append($"{user.LastName[0]}.");

        CurrentUser.Text = AppLocalization.SignedInAsText + userName.ToString();

        MessageBox.Show(AppLocalization.WelcomeMessageText + userName.ToString());
    }
}
}

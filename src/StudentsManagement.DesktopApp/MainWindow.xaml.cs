using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.Common.Enums;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Models;
using StudentsManagement.DesktopApp.Windows.Auth;
using StudentsManagement.DesktopApp.Windows.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace StudentsManagement.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Guid? CurrentUserId;

        private List<AttestationDto> _attestations;

        private List<InfoModel> _specialities;
        private List<InfoModel> _teachers;
        private List<InfoModel> _groups;
        private List<InfoModel> _curriculumUnits;

        private readonly IAuthService _authService;
        private readonly IUsersService _usersService;
        private readonly IWorkTypesService _workTypesService;
        private readonly ISpecialitiesService _specialitiesService;
        private readonly ISubjectsService _subjectsService;
        private readonly ICurriculumUnitsService _curriculumUnitsService;
        private readonly IGroupsService _groupsService;
        private readonly IAttestationsService _attestationsService;
        private readonly IMarksService _marksService;
        private readonly IRetakeResultsService _retakeResultsService;

        public MainWindow(
            IAuthService authService,
            IUsersService usersService,
            IWorkTypesService workTypesService,
            ISpecialitiesService specialitiesService,
            ISubjectsService subjectsService,
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
            _workTypesService = workTypesService;
            _specialitiesService = specialitiesService;
            _subjectsService = subjectsService;
            _curriculumUnitsService = curriculumUnitsService;
            _groupsService = groupsService;
            _attestationsService = attestationsService;
            _marksService = marksService;
            _retakeResultsService = retakeResultsService;

            CurrentUserId = null;

            _specialities = _specialitiesService.GetAll()
                .Select(x => new InfoModel(x.Id, x.FullName)).ToList();
            SpecialitiesComboBox.ItemsSource = _specialities;

            SpecialitiesComboBox.SelectedIndex = 0;

            UpdateEntities();

            UpdateDatagrid();
        }

        private void UpdateDatagrid()
        {
            MainDataGrid.ItemsSource = _attestations;
        }

        private void UpdateEntities()
        {
            var workTypes = _workTypesService.GetAll();
            var subjects = _subjectsService.GetAll();

            _teachers = _usersService.GetUsersWithRole(UserRole.Teacher)
                .Select(x => new InfoModel(x.Id, x.ShortenedName)).ToList();

            var selectedSpeciality = _specialities[SpecialitiesComboBox.SelectedIndex];

            _groups = _groupsService.GetActiveGroups(selectedSpeciality.Id)
                .Select(x => new InfoModel(x.Id, x.Name)).ToList();

            _curriculumUnits = _curriculumUnitsService.GetAll()
                .Select(x =>
                {
                    var workType = workTypes.FirstOrDefault(w => w.Id == x.WorkTypeId).ShortName;
                    var subject = subjects.FirstOrDefault(s => s.Id == x.SubjectId).ShortName;

                    return new InfoModel(x.Id, $"{workType} по предмету {subject} ({x.Name})");
                }).ToList();

            _attestations = new List<AttestationDto>();

            var attestations = _groups.Select(g =>
                _attestationsService.GetActualAttestationsByGroupId(g.Id));

            foreach(var att in attestations)
            {
                _attestations.AddRange(att);
            }

            _attestations = _attestations.OrderBy(x => x.Date).ToList();

            foreach (var entity in _attestations)
            {
                entity.TeacherInfo = _teachers.FirstOrDefault(t => t.Id == entity.TeacherId).Info;
                entity.CurriculutUnitInfo = _curriculumUnits.FirstOrDefault(u => u.Id == entity.CurriculumUnitId).Info;
                entity.GroupInfo = _groups.FirstOrDefault(g => g.Id == entity.GroupId).Info;
            }
        }
        private void SpecialitiesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateEntities();

            UpdateDatagrid();
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
                var currentUserRole = _usersService.GetById(CurrentUserId.Value).Role;

                var user = _usersService.GetById(CurrentUserId.Value);

                var profileWindow = new ProfileWindow(CurrentUserId.Value,
                    currentUserRole == UserRole.Admin || currentUserRole == UserRole.MainAdmin,
                    _specialitiesService,
                    _subjectsService,
                    _groupsService,
                    _workTypesService,
                    _curriculumUnitsService,
                    _attestationsService,
                    _usersService,
                    _marksService,
                    user.GroupId);
                profileWindow.Show();
            }
        }

        private void HandleSuccessfulLogin(object sender, CustomEventArgs e)
        {
            ProfileButton.Content = AppLocalization.ProfileButtonText;
            LogoutButton.Visibility = Visibility.Visible;

            CurrentUserId = e.Id;

            var user = _usersService.GetById(CurrentUserId.Value);

            // получить ФИО пользователя
            var userName = new StringBuilder($"{user.LastName} ");
            if (!string.IsNullOrEmpty(user.MiddleName))
            {
                userName.Append($"{user.MiddleName[0]}.");
            }
            userName.Append($"{user.FirstName[0]}.");

            CurrentUser.Text = AppLocalization.SignedInAsText + userName.ToString();

            MessageBox.Show(AppLocalization.WelcomeMessageText + userName.ToString());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentUserId = null;

            ProfileButton.Content = AppLocalization.LoginButtonText;

            LogoutButton.Visibility = Visibility.Hidden;

            CurrentUser.Text = "";
        }
    }
}

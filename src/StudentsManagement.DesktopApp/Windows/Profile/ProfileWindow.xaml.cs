using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Windows.Attestations;
using StudentsManagement.DesktopApp.Windows.CurriculumUnits;
using StudentsManagement.DesktopApp.Windows.Groups;
using StudentsManagement.DesktopApp.Windows.Specialities;
using StudentsManagement.DesktopApp.Windows.Subjects;
using StudentsManagement.DesktopApp.Windows.Users;
using StudentsManagement.DesktopApp.Windows.WorkTypes;
using System;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Profile
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private readonly Guid _currentUserId;
        private readonly IWorkTypesService _workTypesService;
        private readonly IGroupsService _groupsService;
        private readonly ISpecialitiesService _specialitiesService;
        private readonly ISubjectsService _subjectsService;
        private readonly ICurriculumUnitsService _curriculumUnitsService;
        private readonly IAttestationsService _attestationsService;
        private readonly IUsersService _usersService;
        private readonly IMarksService _marksService;

        public ProfileWindow(Guid currentUserId, bool displayAdminDataButton,
            ISpecialitiesService specialitiesService,
            ISubjectsService subjectsService,
            IGroupsService groupsService,
            IWorkTypesService workTypesService,
            ICurriculumUnitsService curriculumUnitsService,
            IAttestationsService attestationsService,
            IUsersService usersService,
            IMarksService marksService)
        {
            InitializeComponent();

            AdminDataButton.Visibility = displayAdminDataButton ? Visibility.Visible : Visibility.Hidden;

            _currentUserId = currentUserId;

            _specialitiesService = specialitiesService;
            _subjectsService = subjectsService;
            _groupsService = groupsService;
            _workTypesService = workTypesService;
            _curriculumUnitsService = curriculumUnitsService;
            _attestationsService = attestationsService;
            _usersService = usersService;
            _marksService = marksService;
        }

        private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new UsersWindow(_usersService, _groupsService);
            window.Show();
        }

        private void CurriculumUnitsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new CurriculumUnitsWindow(
                _curriculumUnitsService,
                _workTypesService,
                _specialitiesService,
                _subjectsService);
            window.Show();
        }

        private void GroupsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var groupsWindow = new GroupsWindow(_groupsService, _specialitiesService);
            groupsWindow.Show();
        }

        private void AttestationsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new AttestationWindow(
                _attestationsService,
                _curriculumUnitsService,
                _workTypesService,
                _subjectsService,
                _usersService,
                _groupsService,
                _marksService);
            window.Show();
        }

        private void GroupsListsMenuItem_Click(object sender, RoutedEventArgs e)
        { }

        private void WorkTypesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new WorkTypesWindow(_workTypesService);
            window.Show();
        }

        private void SpecialitiesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new SpecialitiesWindow(_specialitiesService);
            window.Show();
        }

        private void SubjectsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new SubjectsWindow(_subjectsService);
            window.Show();
        }
    }
}

using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Windows.Subjects;
using StudentsManagement.DesktopApp.Windows.Faculties;
using StudentsManagement.DesktopApp.Windows.Groups;
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
        private readonly IFacultiesService _workTypesService;
        private readonly IGroupsService _groupsService;
        private readonly ISpecialitiesService _specialitiesService;
        private readonly ISubjectsService _SubjectsService;

        public ProfileWindow(Guid currentUserId,
            IWorkTypesService workTypesService,
            ISpecialitiesService specialitiesService,
            ISubjectsService SubjectsService,
            IGroupsService groupsService)
        {
            InitializeComponent();

            _currentUserId = currentUserId;

            _workTypesService = workTypesService;
            _specialitiesService = specialitiesService;
            _SubjectsService = SubjectsService;
            _groupsService = groupsService;
        }

        private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
        { }

        private void CurriculumUnitsMenuItem_Click(object sender, RoutedEventArgs e)
        { }

        private void GroupsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var groupsWindow = new GroupsWindow(_groupsService, _specialitiesService);
            groupsWindow.Show();
        }

        private void AttestationsMenuItem_Click(object sender, RoutedEventArgs e)
        { }

        private void FacultiesMenuItem_Click(object sender, RoutedEventArgs e)
        {
        //    var facultiesWindow = new FacultiesWindow(_facultiesService, _specialitiesService);
        //    facultiesWindow.Show();
        }

        private void SubjectsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var SubjectsWindow = new SubjectsWindow(_SubjectsService);
            SubjectsWindow.Show();
        }
    }
}

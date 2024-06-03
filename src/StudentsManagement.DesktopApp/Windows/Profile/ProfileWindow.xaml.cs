using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.Common.Enums;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.Models;
using StudentsManagement.DesktopApp.Windows.Attestations;
using StudentsManagement.DesktopApp.Windows.Auth;
using StudentsManagement.DesktopApp.Windows.CurriculumUnits;
using StudentsManagement.DesktopApp.Windows.Groups;
using StudentsManagement.DesktopApp.Windows.Marks;
using StudentsManagement.DesktopApp.Windows.Specialities;
using StudentsManagement.DesktopApp.Windows.Subjects;
using StudentsManagement.DesktopApp.Windows.Users;
using StudentsManagement.DesktopApp.Windows.WorkTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace StudentsManagement.DesktopApp.Windows.Profile
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private readonly Guid _currentUserId;
        private readonly Guid? _groupId;

        private List<AttestationDto> _attestations;

        private List<InfoModel> _teachers;
        private List<InfoModel> _groups;
        private List<(bool, InfoModel)> _curriculumUnits;

        private readonly IWorkTypesService _workTypesService;
        private readonly IGroupsService _groupsService;
        private readonly ISpecialitiesService _specialitiesService;
        private readonly ISubjectsService _subjectsService;
        private readonly ICurriculumUnitsService _curriculumUnitsService;
        private readonly IAttestationsService _attestationsService;
        private readonly IUsersService _usersService;
        private readonly IMarksService _marksService;

        public ProfileWindow(Guid currentUserId, bool userIsAdmin,
            ISpecialitiesService specialitiesService,
            ISubjectsService subjectsService,
            IGroupsService groupsService,
            IWorkTypesService workTypesService,
            ICurriculumUnitsService curriculumUnitsService,
            IAttestationsService attestationsService,
            IUsersService usersService,
            IMarksService marksService,
            Guid? groupId = null)
        {
            InitializeComponent();

            AdminDataButton.Visibility = userIsAdmin ? Visibility.Visible : Visibility.Collapsed;

            _currentUserId = currentUserId;
            _groupId = groupId;

            _specialitiesService = specialitiesService;
            _subjectsService = subjectsService;
            _groupsService = groupsService;
            _workTypesService = workTypesService;
            _curriculumUnitsService = curriculumUnitsService;
            _attestationsService = attestationsService;
            _usersService = usersService;
            _marksService = marksService;

            if (_groupId == null) // преподаватель
            {
                MainDataGrid.Columns[MainDataGrid.Columns.Count - 1].Visibility = Visibility.Hidden; // не отображать колонку оценок
            }

            UpdateEntities();

            UpdateDatagrid();
        }

        private void UpdateDatagrid()
        {
            MainDataGrid.ItemsSource = _attestations;
        }

        private void UpdateEntities()
        {
            var isStudent = _groupId != null;

            var workTypes = _workTypesService.GetAll();
            var subjects = _subjectsService.GetAll();

            _teachers = _usersService.GetUsersWithRole(UserRole.Teacher)
                .Select(x => new InfoModel(x.Id, x.ShortenedName)).ToList();

            List<GroupDto> groups;
            if (isStudent)
            {
                groups = new List<GroupDto>();
                groups.Add(_groupsService.GetById(_groupId.Value));
            }
            else
            {
                groups = _groupsService.GetActiveGroups();
            }

            _groups = groups.Select(x => new InfoModel(x.Id, x.Name)).ToList();

            var curriculumUnits = _curriculumUnitsService.GetAll().ToList();

            _curriculumUnits = curriculumUnits.Select(x =>
            {
                var workType = workTypes.FirstOrDefault(w => w.Id == x.WorkTypeId);
                var subject = subjects.FirstOrDefault(s => s.Id == x.SubjectId).FullName;
                var isBinaryMark = workType.FullName == "Зачёт";

                return (isBinaryMark, new InfoModel(x.Id, $"{workType.ShortName} по предмету {subject} ({x.Name})"));
            }).ToList();


            if (isStudent)
            {
                _attestations = new List<AttestationDto>();

                var attestations = _groups.Select(g => _attestationsService.GetActualAttestationsByGroupId(_groupId.Value));

                foreach (var att in attestations)
                {
                    _attestations.AddRange(att);
                }
            }
            else
            {
                _attestations = _attestationsService.GetActualAttestationsByTeacherId(_currentUserId);
            }

            _attestations = _attestations.OrderBy(x => x.Date).ToList();

            foreach (var att in _attestations)
            {
                var curriculumInfo = _curriculumUnits.FirstOrDefault(u => u.Item2.Id == att.CurriculumUnitId);

                att.TeacherInfo = _teachers.FirstOrDefault(t => t.Id == att.TeacherId).Info;
                att.CurriculutUnitInfo = curriculumInfo.Item2.Info;
                att.GroupInfo = _groups.FirstOrDefault(g => g.Id == att.GroupId).Info;

                if (isStudent && att.Date.Date >= DateTime.Now.Date.AddMonths(-5))
                {
                    // дата аттестации в пределах последних 5 месяцев
                    att.MarkValue = GetMarkValue(_currentUserId, att.Id);
                }
                att.UseBinaryMarks = curriculumInfo.Item1;
            }
        }

        private string GetMarkValue(Guid currentUserId, Guid id)
        {
            var mark = _marksService.GetMarkByUserIdInAttestation(currentUserId, id);

            return _marksService.GetMarkString(mark);
        }

        private void SpecialitiesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateEntities();

            UpdateDatagrid();
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

        private void MainDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_groupId == null)
            {
                var selectedAttestation = GetSelectedItem<AttestationDto>();
                if (selectedAttestation == null)
                    return;

                var window = new AttestationMarksWindow(selectedAttestation.UseBinaryMarks,
                    selectedAttestation, _groupsService, _usersService, _marksService);
                window.Show();
            }
        }

        private void ChangePasswordMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new ChangePasswordWindow(_currentUserId, _usersService);
            window.Show();
        }


        private T GetSelectedItem<T>()
            where T : class, IDto
        {
            var selectedItem = MainDataGrid.SelectedItem as T;

            if (selectedItem == null)
            {
                MessageBox.Show(
                    AppLocalization.SelectSomethingMessageText,
                    AppLocalization.ErrorMessageText);
            }

            return selectedItem;
        }

    }
}

using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.Common.Enums;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Models;
using StudentsManagement.DesktopApp.Windows.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudentsManagement.DesktopApp.Windows.Attestations
{
    /// <summary>
    /// Interaction logic for AttestationWindow.xaml
    /// </summary>
    public partial class AttestationWindow : Window
    {
        private List<AttestationDto> _entities;

        private List<InfoModel> _teachers;
        private List<InfoModel> _groups;
        private List<InfoModel> _curriculumUnits;

        private readonly IAttestationsService _entityService;
        private readonly ICurriculumUnitsService _curriculumUnitsService;
        private readonly IWorkTypesService _workTypesService;
        private readonly IUsersService _usersService;
        private readonly ISubjectsService _subjectsService;
        private readonly IGroupsService _groupsService;
        private readonly IMarksService _marksService;

        public AttestationWindow(
            IAttestationsService entityService,
            ICurriculumUnitsService curriculumUnitsService,
            IWorkTypesService workTypesService,
            ISubjectsService subjectsService,
            IUsersService usersService,
            IGroupsService groupsService,
            IMarksService marksService)
        {
            InitializeComponent();

            _entityService = entityService;
            _curriculumUnitsService = curriculumUnitsService;
            _usersService = usersService;
            _subjectsService = subjectsService;
            _workTypesService = workTypesService;
            _groupsService = groupsService;
            _marksService = marksService;

            UpdateEntities();

            UpdateDropdowns();

            UpdateDatagrid();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateEntities();

            var form = new AttestationForm(AppLocalization.AddAttestationForm, _entityService, _marksService,
                _teachers, _groups, _curriculumUnits);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GetSelectedItem<AttestationDto>();
            if (selectedItem == null) { return; }

            UpdateEntities();

            var form = new AttestationForm(AppLocalization.UpdateCurriculumUnitForm, _entityService, _marksService,
                _teachers, _groups, _curriculumUnits, selectedItem);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = GetSelectedItem<AttestationDto>();
                if (selectedItem == null) { return; }

                var form = new DeleteConfirmation(selectedItem.Id,
                    new List<string>
                    {
                        $"Аттестационное мероприятие",
                        $"Ед. уч. плана: {selectedItem.CurriculutUnitInfo}",
                        $"Преподаватель: {selectedItem.TeacherInfo}",
                        $"Дата: {selectedItem.Date}",
                    });

                form.OnConfirm += HandleDelete;

                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TeacherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDatagrid();

            ClearTeacher.Visibility = TeacherComboBox.SelectedItem != null ? Visibility.Visible : Visibility.Hidden;
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDatagrid();

            ClearGroup.Visibility = GroupComboBox.SelectedItem != null ? Visibility.Visible : Visibility.Hidden;
    }

        private void UnitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDatagrid();

            ClearUnit.Visibility = UnitComboBox.SelectedItem != null ? Visibility.Visible : Visibility.Hidden;
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDatagrid();

            ClearDate.Visibility = DatePicker.SelectedDate != null ? Visibility.Visible : Visibility.Hidden;
        }

        private void MainDataGrid_SelectionDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = GetSelectedItem<AttestationDto>();
            if(selectedItem == null)
            {  return; }

            var marksWindow = new AttestationMarksWindow();
        }

        private void HandleDelete(object sender, CustomEventArgs e)
        {
            _entityService.Delete(e.Id);

            UpdateEntities();
            UpdateDatagrid();
        }

        private void UpdateDropdowns()
        {
            TeacherComboBox.ItemsSource = _teachers;
            GroupComboBox.ItemsSource= _groups;
            UnitComboBox.ItemsSource = _curriculumUnits;
        }

        private void UpdateDatagrid()
        {
            IEnumerable<AttestationDto> listItems = _entities;

            if (TeacherComboBox.SelectedItem != null)
            {
                var selectedTeacher = TeacherComboBox.SelectedItem as InfoModel;
                listItems = listItems.Where(a => a.TeacherId == selectedTeacher.Id);
            }

            if (GroupComboBox.SelectedItem != null)
            {
                var selectedGroup = GroupComboBox.SelectedItem as InfoModel;
                listItems = listItems.Where(a => a.GroupId == selectedGroup.Id);
            }

            if (UnitComboBox.SelectedItem != null)
            {
                var selectedUnit = UnitComboBox.SelectedItem as InfoModel;
                listItems = listItems.Where(a => a.CurriculumUnitId == selectedUnit.Id);
            }

            if (DatePicker.SelectedDate != null)
            {
                var selectedDate = DatePicker.SelectedDate.Value;

                listItems = listItems.Where(a =>
                    a.Date.Day == selectedDate.Day
                    && a.Date.Month == selectedDate.Month
                    && a.Date.Year == selectedDate.Year);
            };

            MainDataGrid.ItemsSource = listItems;
        }

        private void UpdateEntities()
        {
            var workTypes = _workTypesService.GetAll();
            var subjects = _subjectsService.GetAll();

            _teachers = _usersService.GetUsersWithRole(UserRole.Teacher)
                .Select(x => new InfoModel(x.Id, x.ShortenedName)).ToList();

            _groups = _groupsService.GetActiveGroups()
                .Select(x => new InfoModel(x.Id, x.Name)).ToList();

            _curriculumUnits = _curriculumUnitsService.GetAll()
                .Select(x => {
                    var workType = workTypes.FirstOrDefault(w => w.Id == x.WorkTypeId).ShortName;
                    var subject = subjects.FirstOrDefault(s => s.Id == x.SubjectId).ShortName;

                    return new InfoModel(x.Id, $"{workType} по предмету {subject} ({x.Name})");
                }).ToList();

            _entities = _entityService.GetAll().ToList();

            foreach(var entity in _entities)
            {
                entity.TeacherInfo = _teachers.FirstOrDefault(t => t.Id == entity.TeacherId).Info;
                entity.CurriculutUnitInfo = _curriculumUnits.FirstOrDefault(u => u.Id == entity.CurriculumUnitId).Info;
                entity.GroupInfo = _groups.FirstOrDefault(g => g.Id == entity.GroupId).Info;
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        { }

        private void HandleChanges(object sender, CustomEventArgs e)
        {
            UpdateEntities();

            UpdateDropdowns();

            UpdateDatagrid();
        }

        private void ClearTeacher_Click(object sender, RoutedEventArgs e)
        {
            TeacherComboBox.SelectedIndex = -1;
            ClearTeacher.Visibility = Visibility.Hidden;
        }

        private void ClearGroup_Click(object sender, RoutedEventArgs e)
        {
            GroupComboBox.SelectedIndex = -1;
            ClearGroup.Visibility = Visibility.Hidden;
        }

        private void ClearUnit_Click(object sender, RoutedEventArgs e)
        {
            UnitComboBox.SelectedIndex = -1;
            ClearUnit.Visibility = Visibility.Hidden;
        }

        private void ClearDate_Click(object sender, RoutedEventArgs e)
        {
            DatePicker.SelectedDate = null;
            ClearDate.Visibility = Visibility.Hidden;
        }
    }
}

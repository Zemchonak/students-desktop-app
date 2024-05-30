using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudentsManagement.DesktopApp.Windows.Groups
{
    /// <summary>
    /// Interaction logic for GroupsWindow.xaml
    /// </summary>
    public partial class GroupsWindow : Window
    {
        private Guid? _selectedSpecialityId;
        private string _selectedSpecialityName;

        private readonly IGroupsService _entityService;
        private readonly ISpecialitiesService _specialitiesService;

        public GroupsWindow(IGroupsService entityService, ISpecialitiesService specialitiesService)
        {
            InitializeComponent();

            _entityService = entityService;
            _specialitiesService = specialitiesService;

            _selectedSpecialityId = null;

            var specialities = _specialitiesService.GetAll();
            var specialitiesItems = specialities.Select(x => new InfoModel(x.Id, x.FullName)).ToList();

            SpecialitiesComboBox.ItemsSource = specialitiesItems;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedSpecialityId == null)
            {
                SpecialitiesComboBox.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show(AppLocalization.SelectDropdownSomethingMessageText, AppLocalization.ErrorMessageText);
                return;
            }

            var form = new GroupForm(AppLocalization.AddGroupForm,
                _entityService, _specialitiesService, _selectedSpecialityId.Value);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSpecialityId == null)
            {
                SpecialitiesComboBox.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show(AppLocalization.SelectDropdownSomethingMessageText, AppLocalization.ErrorMessageText);
                return;
            }

            var selectedGroup = GetSelectedItem<GroupDto>();
            if (selectedGroup == null) { return; }

            var form = new GroupForm(AppLocalization.UpdateGroupForm,
                _entityService, _specialitiesService, _selectedSpecialityId.Value, selectedGroup);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = GetSelectedItem<GroupDto>();
                if (selectedItem == null) { return; }

                var form = new DeleteConfirmation(selectedItem.Id,
                    new List<string>
                    {
                        $"Группа (специальность: {_selectedSpecialityName})",
                        $"Название: {selectedItem.Name}",
                        $"Курс: {selectedItem.Cource}",
                        $"Год поступления: {selectedItem.EnrollYear}",
                        $"Выпустилась: {selectedItem.GraduatedText}",
                    });

                form.OnConfirm += HandleDelete;

                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SpecialitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpecialitiesComboBox.BorderBrush = new SolidColorBrush(Colors.White);

            var selected = SpecialitiesComboBox.SelectedItem as InfoModel;
            _selectedSpecialityId = selected.Id;
            _selectedSpecialityName = selected.Info;

            UpdateDatagrid();
        }

        // Common logic

        private void HandleDelete(object sender, CustomEventArgs e)
        {
            _entityService.Delete(e.Id);

            UpdateDatagrid();
        }

        private void UpdateDatagrid()
        {
            var groupsList = _entityService.GetGroupsBySpecialityId(_selectedSpecialityId.Value);

            MainDataGrid.ItemsSource = groupsList;
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
            UpdateDatagrid();
        }
    }
}

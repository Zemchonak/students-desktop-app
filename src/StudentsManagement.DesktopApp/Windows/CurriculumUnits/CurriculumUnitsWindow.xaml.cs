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

namespace StudentsManagement.DesktopApp.Windows.CurriculumUnits
{
    /// <summary>
    /// Interaction logic for CurriculumUnitsWindow.xaml
    /// </summary>
    public partial class CurriculumUnitsWindow : Window
    {
        private InfoModel _selectedSpeciality;

        private readonly List<InfoModel> _workTypes;
        private readonly List<InfoModel> _subjects;

        private readonly ICurriculumUnitsService _entityService;
        private readonly IWorkTypesService _workTypesService;
        private readonly ISubjectsService _subjectsService;
        private readonly ISpecialitiesService _specialitiesService;

        public CurriculumUnitsWindow(
            ICurriculumUnitsService entityService,
            IWorkTypesService workTypesService,
            ISpecialitiesService specialitiesService,
            ISubjectsService subjectsService)
        {
            InitializeComponent();

            _entityService = entityService;
            _workTypesService = workTypesService;
            _specialitiesService = specialitiesService;
            _subjectsService = subjectsService;

            _workTypes = _workTypesService.GetAll().Select(x => new InfoModel(x.Id, x.ShortName)).ToList();
            _subjects = _subjectsService.GetAll().Select(x => new InfoModel(x.Id, x.ShortName)).ToList();

            _selectedSpeciality = null;

            var specialities = _specialitiesService.GetAll();
            var specialitiesItems = specialities.Select(x => new InfoModel(x.Id, x.FullName)).ToList();

            SpecialitiesComboBox.ItemsSource = specialitiesItems;
            _subjectsService = subjectsService;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSpeciality == null)
            {
                SpecialitiesComboBox.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show(AppLocalization.ErrorMessageText, AppLocalization.SelectDropdownSomethingMessageText);
                return;
            }

            var form = new CurriculumUnitForm(AppLocalization.AddCurriculumUnitForm,
                _entityService, _workTypes, _subjects, _selectedSpeciality);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSpeciality == null)
            {
                SpecialitiesComboBox.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show(AppLocalization.ErrorMessageText, AppLocalization.SelectDropdownSomethingMessageText);
                return;
            }

            var selectedItem = GetSelectedItem<CurriculumUnitDto>();
            if (selectedItem == null) { return; }

            var form = new CurriculumUnitForm(AppLocalization.UpdateCurriculumUnitForm,
                _entityService, _workTypes, _subjects, _selectedSpeciality, selectedItem);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = GetSelectedItem<CurriculumUnitDto>();
                if (selectedItem == null) { return; }

                var form = new DeleteConfirmation(selectedItem.Id,
                    new List<string>
                    {
                        $"Единица учебного плана",
                        $"Специальность: {_selectedSpeciality.Info}",
                        $"Семестр: {selectedItem.Semester}",
                        $"{selectedItem.WorkTypeName} по предмету {selectedItem.SubjectName}",
                        $"Название: {selectedItem.Name}",
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
            _selectedSpeciality = selected;

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
            var entities = _entityService.GetUnitsBySpecialityId(_selectedSpeciality.Id);

            foreach (var entity in entities)
            {
                entity.SubjectName = _subjects.FirstOrDefault(x => x.Id == entity.SubjectId).Info;
                entity.WorkTypeName = _workTypes.FirstOrDefault(x => x.Id == entity.WorkTypeId).Info;
            }

            MainDataGrid.ItemsSource = entities;
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

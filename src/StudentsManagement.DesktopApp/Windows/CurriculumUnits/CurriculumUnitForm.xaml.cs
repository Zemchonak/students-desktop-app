using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace StudentsManagement.DesktopApp.Windows.CurriculumUnits
{
    /// <summary>
    /// Interaction logic for CurriculumUnitForm.xaml
    /// </summary>
    public partial class CurriculumUnitForm : Window
    {
        private Guid? _entityId;

        private readonly List<InfoModel> _workTypes;
        private readonly List<InfoModel> _subjects;
        private readonly InfoModel _speciality;

        private readonly ICurriculumUnitsService _curriculumUnitsService;
        private readonly ISpecialitiesService _specialitiesService;

        public event CustomEventHandler OnSuccess;

        public CurriculumUnitForm(string title,
            ICurriculumUnitsService curriculumUnitsService,
            List<InfoModel> workTypes,
            List<InfoModel> subjects,
            InfoModel speciality,
            CurriculumUnitDto entityToUpdate = null)
        {
            InitializeComponent();

            Title = title;

            _speciality = speciality;
            _curriculumUnitsService = curriculumUnitsService;
            _workTypes = workTypes;
            _subjects = subjects;

            WorkTypeComboBox.ItemsSource = workTypes;
            SubjectComboBox.ItemsSource = subjects;

            SpecialityInfo.Text = speciality.Info;

            if (entityToUpdate != null)
            {
                _entityId = entityToUpdate.Id;

                FillForm(entityToUpdate);
            }
        }

        private void FillForm(CurriculumUnitDto entity)
        {
            Semester.Text = entity.Semester.ToString();
            Name.Text = entity.Name;

            WorkTypeComboBox.SelectedIndex = _workTypes.FindIndex(x => x.Id == entity.WorkTypeId);
            SubjectComboBox.SelectedIndex = _subjects.FindIndex(x => x.Id == entity.SubjectId);
        }

        private CurriculumUnitDto ParseForm()
        {
            var semesterParsed = int.TryParse(Semester.Text, out int semester);
            if (!semesterParsed)
            {
                MessageBox.Show(string.Format(AppLocalization.IncorrectValueText, AppLocalization.CurriculumUnitFields.Semester),
                    AppLocalization.ErrorMessageText);
                return null;
            }

            if(SubjectComboBox.SelectedItem == null)
            {
                MessageBox.Show(string.Format(AppLocalization.IncorrectValueDropdownText, AppLocalization.SubjectFields.Subject),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            var selectedSubject = SubjectComboBox.SelectedItem as InfoModel;

            if(WorkTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show(string.Format(AppLocalization.IncorrectValueDropdownText, AppLocalization.WorkTypeFields.WorkType),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            var selectedWorkType = WorkTypeComboBox.SelectedItem as InfoModel;

            return new CurriculumUnitDto
            {
                SpecialityId = _speciality.Id,
                Semester = semester,
                WorkTypeId = selectedWorkType.Id,
                SubjectId = selectedSubject.Id,
                Name = Name.Text,
            };
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var unit = ParseForm();

            if (unit == null)
                return; // messagebox уже был показан

            if (_entityId == null)
            {
                unit.Id = _curriculumUnitsService.Create(unit);
            }
            else
            {
                unit.Id = _entityId.Value;

                _curriculumUnitsService.Update(unit);
            }

            OnSuccess?.Invoke(this, new CustomEventArgs(unit.Id));
            this.Close();
        }

        private void WorkTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { }

        private void SubjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { }
    }
}

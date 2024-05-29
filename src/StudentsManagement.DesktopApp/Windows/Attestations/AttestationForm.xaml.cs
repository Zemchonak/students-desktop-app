using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace StudentsManagement.DesktopApp.Windows.Attestations
{
    /// <summary>
    /// Interaction logic for AttestationForm.xaml
    /// </summary>
    public partial class AttestationForm : Window
    {
        private Guid? _entityId;

        private readonly List<InfoModel> _teachers;
        private readonly List<InfoModel> _groups;
        private readonly List<InfoModel> _curriculumUnits;

        private readonly IAttestationsService _attestationService;

        public event CustomEventHandler OnSuccess;

        public AttestationForm(string title,
            IAttestationsService attestationService,
            List<InfoModel> teachers,
            List<InfoModel> groups,
            List<InfoModel> curriculumUnits,
            AttestationDto entityToUpdate = null)
        {
            InitializeComponent();

            Title = title;

            _attestationService = attestationService;

            _teachers = teachers;
            _groups = groups;
            _curriculumUnits = curriculumUnits;

            TeacherComboBox.ItemsSource = _teachers;
            GroupComboBox.ItemsSource = _groups;
            UnitComboBox.ItemsSource = _curriculumUnits;

            if (entityToUpdate != null)
            {
                _entityId = entityToUpdate.Id;

                FillForm(entityToUpdate);
            }
        }

        private void FillForm(AttestationDto entity)
        {
            TeacherComboBox.SelectedIndex = _teachers.FindIndex(x => x.Id == entity.TeacherId);
            GroupComboBox.SelectedIndex = _groups.FindIndex(x => x.Id == entity.GroupId);
            UnitComboBox.SelectedIndex = _curriculumUnits.FindIndex(x => x.Id == entity.CurriculumUnitId);

            DatePicker.SelectedDate = DateTime.Now;
        }

        private AttestationDto ParseForm()
        {
            if (TeacherComboBox.SelectedItem == null)
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueDropdownText, AppLocalization.AttestationFields.Teacher),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            var selectedTeacher = TeacherComboBox.SelectedItem as InfoModel;

            if (GroupComboBox.SelectedItem == null)
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueDropdownText, AppLocalization.GroupFields.Group),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            var selectedGroup = GroupComboBox.SelectedItem as InfoModel;

            if (UnitComboBox.SelectedItem == null)
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueDropdownText, AppLocalization.CurriculumUnitFields.CurriculumUnit),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            var selectedUnit = UnitComboBox.SelectedItem as InfoModel;

            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueText, AppLocalization.AttestationFields.Date),
                    AppLocalization.ErrorMessageText);
                return null;
            };

            return new AttestationDto
            {
                TeacherId = selectedTeacher.Id,
                CurriculumUnitId = selectedUnit.Id,
                GroupId = selectedGroup.Id,
                Date = DatePicker.SelectedDate.Value,
            };
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var fromFormEntity = ParseForm();

            if (fromFormEntity == null)
                return; // messagebox уже был показан

            if (_entityId == null)
            {
                fromFormEntity.Id = _attestationService.Create(fromFormEntity);
            }
            else
            {
                fromFormEntity.Id = _entityId.Value;

                _attestationService.Update(fromFormEntity);
            }

            OnSuccess?.Invoke(this, new CustomEventArgs(fromFormEntity.Id));
            this.Close();
        }
    }
}

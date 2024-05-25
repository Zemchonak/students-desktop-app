using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Models;
using System;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Specialities
{
    /// <summary>
    /// Interaction logic for SpecialitiesForm.xaml
    /// </summary>
    public partial class SpecialitiesForm : Window
    {
        private InfoModel _facultyInfo;
        private Guid? entityId;

        private readonly ISpecialitiesService _service;

        public event CustomEventHandler OnSuccess;

        public SpecialitiesForm(string title, InfoModel facultyInfo, ISpecialitiesService service, SpecialityDto entityToUpdate = null)
        {
            InitializeComponent();

            Title = title;
            _facultyInfo = facultyInfo;
            _service = service;

            if (entityToUpdate != null)
            {
                FillForm(entityToUpdate);
            }
        }

        private void FillForm(SpecialityDto entity)
        {
            entityId = entity.Id;
            ShortNameTextBox.Text = entity.ShortName;
            FullNameTextBox.Text = entity.FullName;
        }

        private SpecialityDto ParseForm()
        {
            return new SpecialityDto()
            {
                Id = entityId ?? Guid.Empty,
                ShortName = ShortNameTextBox.Text,
                FullName = FullNameTextBox.Text,
                FacultyId = _facultyInfo.Id,
            };
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var parsedEntity = ParseForm();

            try
            {
                _service.Validate(parsedEntity);

                if (entityId == null)
                {
                    entityId = _service.Create(parsedEntity);
                }
                else
                {
                    _service.Update(parsedEntity);
                }

                OnSuccess?.Invoke(this, new CustomEventArgs(entityId.Value));

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

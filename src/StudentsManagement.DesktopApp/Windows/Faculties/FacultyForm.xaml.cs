using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using System;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Faculties
{
    /// <summary>
    /// Interaction logic for FacultyForm.xaml
    /// </summary>
    public partial class FacultyForm : Window
    {
        private Guid? entityId;

        private readonly IFacultiesService _service;

        public event CustomEventHandler OnSuccess;

        public FacultyForm(string title, IFacultiesService service, FacultyDto entityToUpdate = null)
        {
            InitializeComponent();

            Title = title;
            _service = service;

            if (entityToUpdate != null)
            {
                FillForm(entityToUpdate);
            }
        }

        private void FillForm(FacultyDto entity)
        {
            entityId = entity.Id;
            ShortNameTextBox.Text = entity.ShortName;
            FullNameTextBox.Text = entity.FullName;
        }

        private FacultyDto ParseForm()
        {
            return new FacultyDto()
            {
                Id = entityId ?? Guid.Empty,
                ShortName = ShortNameTextBox.Text,
                FullName = FullNameTextBox.Text,
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

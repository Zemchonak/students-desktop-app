using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentsManagement.DesktopApp.Windows.Subjects
{
    /// <summary>
    /// Interaction logic for SubjectForm.xaml
    /// </summary>
    public partial class SubjectForm : Window
    {
        private Guid? entityId;

        private readonly ISubjectsService _service;

        public event CustomEventHandler OnSuccess;

        public SubjectForm(string title, ISubjectsService service, SubjectDto entityToUpdate = null)
        {
            InitializeComponent();

            Title = title;
            _service = service;

            if (entityToUpdate != null)
            {
                FillForm(entityToUpdate);
            }
        }

        private void FillForm(SubjectDto entity)
        {
            entityId = entity.Id;
            ShortNameTextBox.Text = entity.ShortName;
            FullNameTextBox.Text = entity.FullName;
        }

        private SubjectDto ParseForm()
        {
            return new SubjectDto()
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

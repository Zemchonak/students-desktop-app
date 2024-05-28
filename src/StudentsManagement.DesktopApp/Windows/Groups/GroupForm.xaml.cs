using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DesktopApp.EventHandlers;
using System;
using System.Linq;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Groups
{
    /// <summary>
    /// Interaction logic for GroupForm.xaml
    /// </summary>
    public partial class GroupForm : Window
    {
        private Guid? _entityId;

        private readonly GroupDto _group;
        private readonly Guid _specialityId;

        private readonly IGroupsService _groupService;
        private readonly ISpecialitiesService _specialitiesService;

        public event CustomEventHandler OnSuccess;

        public GroupForm(string title, IGroupsService groupService, ISpecialitiesService specialitiesService, Guid specialityId, GroupDto entityToUpdate = null)
        {
            InitializeComponent();
            GraduatedGroupMessage.Visibility = Visibility.Hidden;

            Title = title;

            _groupService = groupService;
            _specialitiesService = specialitiesService;
            _specialityId = specialityId;

            var speciality = _specialitiesService.GetById(specialityId);

            if (entityToUpdate != null)
            {
                _group = entityToUpdate;
            }
            else
            {
                PromoteButton.IsEnabled = false;
                GraduateButton.IsEnabled = false;

                var currentYear = DateTime.Now.Year;

                _group = new GroupDto
                {
                    Name = "",
                    Cource = 1,
                    EnrollYear = currentYear,
                    Graduated = false,
                    SpecialityId = _specialityId,
                };
            }

            FillForm(speciality.FullName);

            if(_group.Graduated)
            {
                PromoteButton.IsEnabled = false;
                GraduateButton.IsEnabled = false;
                SaveButton.IsEnabled = false;

                GraduatedGroupMessage.Visibility = Visibility.Visible;
            }
        }

        private void FillForm(string specialityName)
        {
            _entityId = _group.Id;

            SpecialityInfo.Text = specialityName;
            Name.Text = _group.Name;
            EnrollYear.Text = _group.EnrollYear.ToString();
            Cource.Text = _group.Cource.ToString();
        }

        private void PromoteButton_Click(object sender, RoutedEventArgs e)
        {
            _group.Cource += 1;

            _groupService.Update(_group);

            OnSuccess?.Invoke(this, new CustomEventArgs(_group.Id));
            this.Close();
        }

        private void GraduateButton_Click(object sender, RoutedEventArgs e)
        {
            _group.Graduated = true;

            _groupService.Update(_group);

            OnSuccess?.Invoke(this, new CustomEventArgs(_group.Id));
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _group.Name = Name.Text;

            if(_group.Id == Guid.Empty)
            {
                _groupService.Create(_group);
            }
            else
            {
                _groupService.Update(_group);
            }

            OnSuccess?.Invoke(this, new CustomEventArgs(_group.Id));
            this.Close();
        }
    }
}

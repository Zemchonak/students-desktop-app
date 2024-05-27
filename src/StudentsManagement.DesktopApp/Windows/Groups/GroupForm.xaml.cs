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

            CreateButton.Visibility = Visibility.Hidden;

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
                var currentYear = DateTime.Now.Year;

                var groups = _groupService.GetGroupsBySpecialityId(specialityId)
                    .Where(x => x.EnrollYear == currentYear).ToArray();

                _group = new GroupDto
                {
                    SpecialityShortName = speciality.ShortName,
                    EnrollYear = currentYear,
                    Graduated = false,
                    SpecialityId = _specialityId,
                    Cource = 1,
                    Number = groups.Length + 1,
                };

                CreateButton.Visibility = Visibility.Visible;
            }

            FillForm(speciality.FullName);

            if(_group.Graduated)
            {
                PromoteButton.IsEnabled = false;
                GraduateButton.IsEnabled = false;

                GraduatedGroupMessage.Visibility = Visibility.Visible;
            }
        }

        private void FillForm(string specialityName)
        {
            _entityId = _group.Id;

            SpecialityInfo.Text = specialityName;
            EnrollYear.Text = _group.EnrollYear.ToString();
            Cource.Text = _group.Cource.ToString();
            Number.Text = _group.Number.ToString();
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

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            _groupService.Create(_group);

            OnSuccess?.Invoke(this, new CustomEventArgs(_group.Id));
            this.Close();
        }
    }
}

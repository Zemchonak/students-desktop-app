using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.Common.Enums;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.EventHandlers;
using System.Linq;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Users
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        private readonly IUsersService _usersService;
        private readonly IGroupsService _groupsService;

        public UsersWindow(IUsersService usersService, IGroupsService groupsService)
        {
            InitializeComponent();
            _usersService = usersService;
            _groupsService = groupsService;

            UpdateDatagrid();
        }

        private void EditSelected_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GetSelectedItem<UserDto>();
            if (selectedItem == null) { return; }

            //var form = new AttestationForm(AppLocalization.UpdateCurriculumUnitForm,
            //    _entityService, _teachers, _groups, _curriculumUnits, selectedItem);
            //form.OnSuccess += HandleChanges;
            //form.Show();
        }

        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            var form = new UserForm(AppLocalization.AddUserForm, _usersService, _groupsService);
            form.OnSuccess += HandleChanges;
            form.Show();
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

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HandleChanges(object sender, CustomEventArgs e)
        {
            UpdateDatagrid();
        }

        private void UpdateDatagrid()
        {
            var users = _usersService.GetAll().Where(x => x.Role != UserRole.MainAdmin);

            foreach (var user in users)
            {
                if(user.Role == UserRole.Student)
                {
                    var group = _groupsService.GetById(user.GroupId.Value);
                    user.GroupId = group.Id;
                    user.GroupName = group.Name;
                }
            }

            MainDataGrid.ItemsSource = users;
        }
    }
}

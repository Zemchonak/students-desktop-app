using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.Common.Enums;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Helpers;
using StudentsManagement.DesktopApp.Models;
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

namespace StudentsManagement.DesktopApp.Windows.Users
{
    /// <summary>
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {
        private readonly Guid? _entityId;
        private string _password;

        private List<InfoModel> _groups;

        private readonly List<InfoModel> _roles = new()
        {
            new InfoModel(Guid.NewGuid(), AppLocalization.Roles.Student),
            new InfoModel(Guid.NewGuid(), "Преподаватель"),
            new InfoModel(Guid.NewGuid(), "Администратор"),
        };
        private readonly IUsersService _usersService;
        private readonly IGroupsService _groupsService;

        public event CustomEventHandler OnSuccess;

        public UserForm(string title, IUsersService usersService, IGroupsService groupsService, UserDto entityToUpdate = null)
        {
            InitializeComponent();

            Title = title;

            _usersService = usersService;
            _groupsService = groupsService;

            _groups = _groupsService.GetActiveGroups().Select(x => new InfoModel(x.Id, x.Name)).ToList();

            RoleComboBox.ItemsSource = _roles;

            if (entityToUpdate != null)
            {
                _entityId = entityToUpdate.Id;
                _password = entityToUpdate.PasswordHash;

                GeneratePasswordButton.Visibility = Visibility.Hidden;
                SaveButton.Visibility = Visibility.Visible;

                FillForm(entityToUpdate);
            }
        }

        private void FillForm(UserDto entity)
        {
            FirstNameInput.Text = entity.FirstName;
            MiddleNameInput.Text = entity.MiddleName;
            LastNameInput.Text = entity.LastName;
            EmailInput.Text = entity.Email;

            var role = (int)entity.Role;
            RoleComboBox.SelectedIndex = role;

            if (entity.Role == UserRole.Student)
            {
                GroupComboBox.SelectedIndex = _groups.FindIndex(x => x.Id == entity.GroupId);
            }
        }

        private UserDto ParseForm()
        {
            var userDto = new UserDto();

            if (string.IsNullOrEmpty(EmailInput.Text))
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueText, AppLocalization.UserFields.Email),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            userDto.Email = EmailInput.Text;

            if (string.IsNullOrEmpty(FirstNameInput.Text))
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueText, AppLocalization.UserFields.FirstName),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            userDto.FirstName = FirstNameInput.Text;

            if (string.IsNullOrEmpty(MiddleNameInput.Text))
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueText, AppLocalization.UserFields.MiddleName),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            userDto.MiddleName = MiddleNameInput.Text;

            if (string.IsNullOrEmpty(LastNameInput.Text))
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueText, AppLocalization.UserFields.LastName),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            userDto.LastName = LastNameInput.Text;

            var selectedRole = RoleComboBox.SelectedItem as InfoModel;
            if (selectedRole == null)
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueDropdownText, AppLocalization.UserFields.Role),
                    AppLocalization.ErrorMessageText);
                return null;
            }
            else
            {
                var roleValue = _roles.FindIndex(r => r.Id == selectedRole.Id);
                var role = (UserRole)(roleValue+1);

                if (role == UserRole.Student)
                {

                    var selectedGroup = GroupComboBox.SelectedItem as InfoModel;
                    if (selectedGroup == null)
                    {
                        MessageBox.Show(
                            string.Format(AppLocalization.IncorrectValueDropdownText, AppLocalization.UserFields.Group),
                            AppLocalization.ErrorMessageText);
                        return null;
                    }

                    userDto.GroupId = selectedGroup.Id;
                }

                userDto.Role = role;
            }

            return userDto;
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

            if (string.IsNullOrEmpty(_password))
            {
                MessageBox.Show(
                    string.Format(AppLocalization.IncorrectValueDropdownText, AppLocalization.UserFields.PasswordHash),
                    AppLocalization.ErrorMessageText);
                return;
            }
            fromFormEntity.PasswordHash = _password;

            if (_entityId == null)
            {
                fromFormEntity.Id = _usersService.Create(fromFormEntity);
            }
            else
            {
                fromFormEntity.Id = _entityId.Value;

                _usersService.Update(fromFormEntity);
            }

            OnSuccess?.Invoke(this, new CustomEventArgs(fromFormEntity.Id));
            this.Close();
        }

        private void RoleComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedItem = RoleComboBox.SelectedItem as InfoModel;

            if (selectedItem.Id == _roles[0].Id)
            {
                GroupComboBox.Visibility = Visibility.Visible;
                GroupLabel.Visibility = Visibility.Visible;

                GroupComboBox.ItemsSource = _groups;
            }
            else if (GroupComboBox.Visibility != Visibility.Hidden)
            {
                GroupComboBox.Visibility = Visibility.Hidden;
                GroupLabel.Visibility = Visibility.Hidden;
            }
        }

        private void GeneratePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var passwordWindow = new PasswordWindow();
            passwordWindow.OnSuccess += HandlePasswordGeneration;
            passwordWindow.Show();

            void HandlePasswordGeneration(object sender, CustomEventArgs e)
            {
                _password = AuthHelper.CreateSha256Hash(e.Message);

                GeneratePasswordButton.Visibility = Visibility.Hidden;
                SaveButton.Visibility = Visibility.Visible;
            }
        }
    }
}

using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Common;
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
using static StudentsManagement.DesktopApp.Common.AppLocalization;

namespace StudentsManagement.DesktopApp.Windows.Marks
{
    /// <summary>
    /// Interaction logic for AttestationMarksWindow.xaml
    /// </summary>
    public partial class AttestationMarksWindow : Window
    {
        private bool _useBinaryMarks;
        private AttestationDto _selectedAttestation;
        private IGroupsService _groupsService;
        private IUsersService _usersService;
        private IMarksService _marksService;

        public AttestationMarksWindow(bool binaryMarks, AttestationDto selectedAttestation,
            IGroupsService groupsService, IUsersService usersService, IMarksService marksService)
        {
            InitializeComponent();

            _useBinaryMarks = binaryMarks;
            _selectedAttestation = selectedAttestation;
            _groupsService = groupsService;
            _usersService = usersService;
            _marksService = marksService;

            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            var title = $"Оценки группы {_selectedAttestation.GroupInfo} по " +
                $"{_selectedAttestation.CurriculutUnitInfo} ({_selectedAttestation.FormattedDate})";
            Title = title;
            TitleText.Text = title;

            var marks = _marksService.GetMarksByAttestationId(_selectedAttestation.Id);
            var groupStudents = _usersService.GetUsersByGroupId(_selectedAttestation.GroupId);

            foreach (var student in groupStudents)
            {
                var markValue = marks.FirstOrDefault(m =>
                    m.StudentId == student.Id);
                student.MarkValue = _marksService.GetMarkString(markValue, _useBinaryMarks);
            }

            MainDataGrid.ItemsSource = groupStudents;
        }

        private void MainDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = GetSelectedItem<UserDto>();
            if (selectedUser == null)
            { return; }

            var existingMark = _marksService.GetMarkByUserIdInAttestation(selectedUser.Id, _selectedAttestation.Id);

            var window = new PutMarkWindow(_useBinaryMarks, _selectedAttestation, selectedUser,
                _marksService, existingMark);
            window.OnSuccess += HandleUpdates;

            window.Show();
        }

        private void HandleUpdates(object sender, CustomEventArgs e)
        {
            UpdateDataGrid();
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
    }
}

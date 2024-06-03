using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Marks
{
    /// <summary>
    /// Interaction logic for PutMarkWindow.xaml
    /// </summary>
    public partial class PutMarkWindow : Window
    {
        private readonly bool _useBinaryMarks;
        private AttestationDto _selectedAttestation;
        private UserDto _selectedUser;
        private readonly IMarksService _marksService;

        private MarkDto _mark;

        public event CustomEventHandler OnSuccess;

        public PutMarkWindow(bool useBinaryMarks, AttestationDto selectedAttestation, UserDto selectedUser, IMarksService marksService, MarkDto existingMark = null)
        {
            InitializeComponent();

            _useBinaryMarks = useBinaryMarks;
            _selectedAttestation = selectedAttestation;
            _selectedUser = selectedUser;
            _marksService = marksService;

            Title += selectedUser.ShortenedName;
            TitleText.Text = Title;

            _mark = existingMark;

            if(useBinaryMarks)
            {
                NotAcceptedButton.Visibility = Visibility.Visible;
                AcceptedButton.Visibility = Visibility.Visible;
            }
            else
            {
                MarkValueLabel.Visibility = Visibility.Visible;
                MarkValue.Visibility = Visibility.Visible;
                ConfirmButton.Visibility = Visibility.Visible;
            }

            if(existingMark != null && existingMark.Value != null)
            {
                MarkValue.Text = existingMark.Value.ToString();
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var parsed = int.TryParse(MarkValue.Text, out int value);

            UpsertMark(value);

            OnSuccess?.Invoke(this, new CustomEventArgs(_mark.Id));
            this.Close();
        }

        private void NotAcceptedButton_Click(object sender, RoutedEventArgs e)
        {
            UpsertMark(false);

            OnSuccess?.Invoke(this, new CustomEventArgs(_mark.Id));
            this.Close();
        }

        private void AcceptedButton_Click(object sender, RoutedEventArgs e)
        {
            UpsertMark(true);

            OnSuccess?.Invoke(this, new CustomEventArgs(_mark.Id));
            this.Close();
        }

        private void UpsertMark(int value)
        {
            if (_mark == null)
            {
                _mark = new MarkDto
                {
                    Value = value,
                    NotAllowed = null,
                    NotAttended = null,
                    StudentId = _selectedUser.Id,
                    AttestationId = _selectedAttestation.Id
                };

                _marksService.Create(_mark);
            }
            else
            {
                _mark.Value = value;

                _mark.NotAllowed = null;
                _mark.NotAttended = null;
                _marksService.Update(_mark);
            }
        }

        private void UpsertMark(bool value)
        {
            var acceptedValue = value ? 1 : 0;
            if (_mark == null)
            {
                _mark = new MarkDto
                {
                    Value = acceptedValue,
                    NotAllowed = null,
                    NotAttended = null,
                    StudentId = _selectedUser.Id,
                    AttestationId = _selectedAttestation.Id
                };

                _marksService.Create(_mark);
            }
            else
            {
                _mark.Value = acceptedValue;
                _mark.NotAllowed = null;
                _mark.NotAttended = null;
                _marksService.Update(_mark);
            }
        }

        private void UpsertNotMark(bool? notAttended = null, bool? notAllowed = null)
        {
            if (_mark == null)
            {
                _mark = new MarkDto
                {
                    Value = null,
                    NotAttended = notAttended,
                    NotAllowed = notAllowed,
                    StudentId = _selectedUser.Id,
                    AttestationId = _selectedAttestation.Id
                };

                _marksService.Create(_mark);
            }
            else
            {
                _mark.NotAttended = notAttended;
                _mark.NotAllowed = notAllowed;
                _marksService.Update(_mark);
            }
        }

        private void MarkValue_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ConfirmButton.Visibility = int.TryParse(MarkValue.Text, out int value) && value >= 0 && value < 11 ?
                Visibility.Visible : Visibility.Collapsed;
        }

        private void NotAllowedButton_Click(object sender, RoutedEventArgs e)
        {
            UpsertNotMark(notAllowed: true);

            OnSuccess?.Invoke(this, new CustomEventArgs(_mark.Id));
            this.Close();
        }

        private void NotAttendedButton_Click(object sender, RoutedEventArgs e)
        {
            UpsertNotMark(notAttended: true);

            OnSuccess?.Invoke(this, new CustomEventArgs(_mark.Id));
            this.Close();
        }
    }
}

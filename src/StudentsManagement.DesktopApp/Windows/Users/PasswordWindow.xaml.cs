using StudentsManagement.DesktopApp.EventHandlers;
using System;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Users
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public event CustomEventHandler OnSuccess;

        public PasswordWindow()
        {
            InitializeComponent();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var guid = Guid.NewGuid().ToString()[..12];
            Clipboard.SetText(guid);

            OnSuccess?.Invoke(this, new CustomEventArgs(Guid.Empty, guid));
            this.Close();
        }
    }
}

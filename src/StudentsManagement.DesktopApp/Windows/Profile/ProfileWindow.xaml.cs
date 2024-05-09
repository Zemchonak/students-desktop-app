using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Windows.Faculties;
using System;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Profile
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private readonly Guid _currentUserId;
        private readonly IFacultiesService _facultiesService;

        public ProfileWindow(Guid currentUserId, IFacultiesService facultiesService)
        {
            InitializeComponent();

            _currentUserId = currentUserId;
            _facultiesService = facultiesService;
        }

        private void FacultiesBtn_Click(object sender, RoutedEventArgs e)
        {
            var facultiesWindow = new FacultiesWindow(_facultiesService);
            facultiesWindow.Show();
        }
    }
}

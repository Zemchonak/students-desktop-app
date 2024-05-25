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
        private readonly ISpecialitiesService _specialitiesService;

        public ProfileWindow(Guid currentUserId, IFacultiesService facultiesService, ISpecialitiesService specialitiesService)
        {
            InitializeComponent();

            _currentUserId = currentUserId;
            _facultiesService = facultiesService;
            _specialitiesService = specialitiesService;
        }

        private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
        { }

        private void FacultiesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var facultiesWindow = new FacultiesWindow(_facultiesService, _specialitiesService);
            facultiesWindow.Show();
        }

        private void DisciplinesMenuItem_Click(object sender, RoutedEventArgs e)
        { }
    }
}

using System;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Subjects
{
    /// <summary>
    /// Interaction logic for SubjectWindow.xaml
    /// </summary>
    public partial class SubjectWindow : Window
    {
        private readonly ISpecialitiesService _specialitiesService;
        private readonly IFacultiesService _entityService;

        public SubjectWindow(ISubjectsService subjectsService)
        {
            InitializeComponent();
            _entityService = subjectsService;
            _specialitiesService = specialitiesService;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new SubjectForm(AppLocalization.AddFacultyForm, _entityService);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GetSelectedItem<FacultyDto>();
            if (selectedItem == null) { return; }

            var form = new FacultyForm(AppLocalization.UpdateFacultyForm, _entityService, selectedItem);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = GetSelectedItem<FacultyDto>();
                if (selectedItem == null) { return; }

                var form = new DeleteConfirmation(selectedItem.Id,
                    new List<string>
                    {
                        $"Факультет",
                        $"Кр. назв.: {selectedItem.ShortName}",
                        $"Полное назв.: {selectedItem.FullName}",
                    });

                form.OnConfirm += HandleDelete;

                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedItem = GetSelectedItem<FacultyDto>();
            if (selectedItem == null) { return; }

            var specialitiesWindow = new SpecialitiesWindow(new InfoModel(selectedItem.Id, selectedItem.ShortName), _specialitiesService);
            specialitiesWindow.Show();
        }

        // Common logic

        private void HandleDelete(object sender, CustomEventArgs e)
        {
            _entityService.Delete(e.Id);

            UpdateDatagrid();
        }

        private void UpdateDatagrid()
        {
            var items = _entityService.GetAll();

            MainDataGrid.ItemsSource = items;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDatagrid();
        }

        private void HandleChanges(object sender, CustomEventArgs e)
        {
            UpdateDatagrid();
        }
    }
}

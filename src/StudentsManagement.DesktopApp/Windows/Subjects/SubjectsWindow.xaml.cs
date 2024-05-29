using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.Common;
using StudentsManagement.DesktopApp.EventHandlers;
using System;
using System.Collections.Generic;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Subjects
{
    /// <summary>
    /// Interaction logic for SubjectsWindow.xaml
    /// </summary>
    public partial class SubjectsWindow : Window
    {
        private readonly ISubjectsService _entityService;

        public SubjectsWindow(ISubjectsService SubjectsService)
        {
            InitializeComponent();
            _entityService = SubjectsService;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new SubjectForm(AppLocalization.AddSubjectForm, _entityService);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GetSelectedItem<SubjectDto>();
            if (selectedItem == null) { return; }

            var form = new SubjectForm(AppLocalization.UpdateSubjectForm, _entityService, selectedItem);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = GetSelectedItem<SubjectDto>();

                var form = new DeleteConfirmation(selectedItem.Id,
                    new List<string>
                    {
                        $"Дисциплина",
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


using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Utils;
using System;
using System.Collections.Generic;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows.Faculties
{
    /// <summary>
    /// Interaction logic for FacultiesWindow.xaml
    /// </summary>
    public partial class FacultiesWindow : Window
    {
        private readonly IFacultiesService _facultiesService;

        public FacultiesWindow(IFacultiesService facultiesService)
        {
            InitializeComponent();
            _facultiesService = facultiesService;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDatagrid();
        }

        private void HandleChanges(object sender, CustomEventArgs e)
        {
            UpdateDatagrid();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new FacultyForm(AppLocalization.AddFacultyForm, _facultiesService);
            form.OnSuccess += HandleChanges;
            form.Show();
        }



        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            // get selected faculty
            var selectedItem = MainDataGrid.SelectedItem as FacultyDto;

            var form = new FacultyForm(AppLocalization.UpdateFacultyForm, _facultiesService, selectedItem);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = MainDataGrid.SelectedItem as FacultyDto;

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

        private void HandleDelete(object sender, CustomEventArgs e)
        {
            _facultiesService.Delete(e.Id);

            UpdateDatagrid();
        }

        private void UpdateDatagrid()
        {
            var items = _facultiesService.GetAll();

            MainDataGrid.ItemsSource = items;
        }
    }
}

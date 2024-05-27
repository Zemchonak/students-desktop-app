using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Models;
using StudentsManagement.DesktopApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace StudentsManagement.DesktopApp.Windows.Specialities
{
    /// <summary>
    /// Interaction logic for SpecialitiesWindow.xaml
    /// </summary>
    public partial class SpecialitiesWindow : Window
    {
        private InfoModel _facultyInfo;

        private readonly ISpecialitiesService _entityService;

        public SpecialitiesWindow(InfoModel facultyInfo, ISpecialitiesService specialitiesService)
        {
            InitializeComponent();

            Title = $"Cпециальности факультета {facultyInfo.Info}";
            HeaderText.Text = Title;

            _facultyInfo = facultyInfo;
            _entityService = specialitiesService;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new SpecialitiesForm(AppLocalization.AddSpecialityForm,
                _facultyInfo,
                _entityService);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GetSelectedItem<SpecialityDto>();
            if (selectedItem == null) { return; }

            var form = new SpecialitiesForm(AppLocalization.UpdateFacultyForm,
                _facultyInfo,
                _entityService,
                selectedItem);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = GetSelectedItem<SpecialityDto>();
                if (selectedItem == null) { return; }

                var form = new DeleteConfirmation(selectedItem.Id,
                    new List<string>
                    {
                        $"Специальность",
                        $"Факультет: {_facultyInfo.Info}",
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

        private void MainDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = GetSelectedItem<SpecialityDto>();
            if (selectedItem == null) { return; }

            var childWindow = new SpecialitiesWindow(
                new InfoModel(selectedItem.Id, selectedItem.ShortName),
                _entityService);
            childWindow.Show();
        }

        // Common logic

        private void HandleDelete(object sender, CustomEventArgs e)
        {
            _entityService.Delete(e.Id);

            UpdateDatagrid();
        }

        private void UpdateDatagrid()
        {
            var items = _entityService.GetAll().OrderBy(x => x.FullName);

            MainDataGrid.ItemsSource = items;
        }

        private T GetSelectedItem<T>()
            where T: class, IDto
        {
            var selectedItem = MainDataGrid.SelectedItem as T;

            if(selectedItem == null)
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

using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using StudentsManagement.DesktopApp.Models;
using StudentsManagement.DesktopApp.Utils;
using StudentsManagement.DesktopApp.Windows.Faculties;
using StudentsManagement.DesktopApp.Windows.Specialities;
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

namespace StudentsManagement.DesktopApp.Windows.Disciplines
{
    /// <summary>
    /// Interaction logic for DisciplinesWindow.xaml
    /// </summary>
    public partial class DisciplinesWindow : Window
    {
        private readonly IDisciplinesService _entityService;

        public DisciplinesWindow(IDisciplinesService disciplinesService)
        {
            InitializeComponent();
            _entityService = disciplinesService;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new DisciplineForm(AppLocalization.AddDisciplineForm, _entityService);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GetSelectedItem<DisciplineDto>();
            if (selectedItem == null) { return; }

            var form = new DisciplineForm(AppLocalization.UpdateDisciplineForm, _entityService, selectedItem);
            form.OnSuccess += HandleChanges;
            form.Show();
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = GetSelectedItem<DisciplineDto>();

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

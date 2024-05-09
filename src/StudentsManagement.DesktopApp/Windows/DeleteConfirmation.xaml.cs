using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DesktopApp.EventHandlers;
using System;
using System.Collections.Generic;
using System.Windows;

namespace StudentsManagement.DesktopApp.Windows
{
    /// <summary>
    /// Interaction logic for DeleteConfirmation.xaml
    /// </summary>
    public partial class DeleteConfirmation : Window
    {
        private readonly Guid _entityId;

        public event CustomEventHandler OnConfirm;

        public DeleteConfirmation(Guid entityId, List<string> entityInfo)
        {
            InitializeComponent();

            EntityValues.Text = string.Join("\n", entityInfo);
            _entityId = entityId;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            OnConfirm?.Invoke(this, new CustomEventArgs(_entityId));

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

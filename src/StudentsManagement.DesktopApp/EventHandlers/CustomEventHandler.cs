using System;

namespace StudentsManagement.DesktopApp.EventHandlers
{
    public class CustomEventArgs : EventArgs
    {
        public Guid Id { get; set; }
        public string Message { get; set; }

        public CustomEventArgs(Guid id, string message = null)
        {
            Id = id;
            Message = message;
        }
    }

    public delegate void CustomEventHandler(object sender, CustomEventArgs e);
}

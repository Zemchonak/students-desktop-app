using System;

namespace StudentsManagement.DesktopApp.EventHandlers
{
    // Custom event arguments class
    public class CustomEventArgs : EventArgs
    {
        public string Id { get; set; }  
        public string Message { get; set; }
    }

    // Custom event handler delegate
    public delegate void CustomEventHandler(object sender, CustomEventArgs e);
}

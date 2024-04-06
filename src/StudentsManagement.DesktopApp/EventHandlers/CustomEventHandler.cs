using System;

namespace StudentsManagement.DesktopApp.EventHandlers
{
    public class CustomEventArgs : EventArgs
    {
        public string Id { get; set; }  
        public string Message { get; set; }
    }

    public delegate void CustomEventHandler(object sender, CustomEventArgs e);
}

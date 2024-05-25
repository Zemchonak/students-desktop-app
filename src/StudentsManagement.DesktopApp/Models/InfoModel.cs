using System;

namespace StudentsManagement.DesktopApp.Models
{
    public class InfoModel
    {
        public Guid Id { get; set; }
        public string Info { get; set; }

        public InfoModel(Guid id, string info)
        {
            Id = id;
            Info = info;
        }
    }
}

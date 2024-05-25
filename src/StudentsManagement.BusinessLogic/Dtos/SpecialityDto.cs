﻿namespace StudentsManagement.BusinessLogic.Dtos
{
    public class SpecialityDto : BaseDto
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public Guid FacultyId { get; set; }
    }
}

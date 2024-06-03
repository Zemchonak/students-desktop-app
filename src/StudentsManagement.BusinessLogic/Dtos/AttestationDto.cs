namespace StudentsManagement.BusinessLogic.Dtos
{
    public class AttestationDto : BaseDto
    {
        public Guid TeacherId { get; set; }
        public string TeacherInfo { get; set; }
        public Guid GroupId { get; set; }
        public string GroupInfo { get; set; }
        public Guid CurriculumUnitId { get; set; }
        public string CurriculutUnitInfo { get; set; }
        public DateTime Date { get; set; }
        public string FormattedDate { get => Date.ToString("D"); }

        // для особых двух отметок у зачёта
        public bool UseBinaryMarks { get; set; }

        // для личного кабинета студента
        public string MarkValue { get; set; }
    }
}

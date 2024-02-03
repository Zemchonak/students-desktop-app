namespace StudentsManagement.BusinessLogic.Dtos
{
    public class AttestationDto : BaseDto
    {
        public string TeacherId { get; set; }
        public string GroupId { get; set; }
        public string CurriculumUnitId { get; set; }
        public DateTime Date { get; set; }
    }
}

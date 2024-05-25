namespace StudentsManagement.BusinessLogic.Dtos
{
    public class AttestationDto : BaseDto
    {
        public Guid TeacherId { get; set; }
        public Guid GroupId { get; set; }
        public Guid CurriculumUnitId { get; set; }
        public DateTime Date { get; set; }
    }
}

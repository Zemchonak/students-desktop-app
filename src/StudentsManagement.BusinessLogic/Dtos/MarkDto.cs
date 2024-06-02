namespace StudentsManagement.BusinessLogic.Dtos
{
    public class MarkDto : BaseDto
    {
        public Guid StudentId { get; set; }
        public Guid AttestationId { get; set; }
        public int? Value { get; set; }
        public bool? NotAttended { get; set; }
        public bool? NotAllowed { get; set; }
    }
}

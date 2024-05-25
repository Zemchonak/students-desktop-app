namespace StudentsManagement.BusinessLogic.Dtos
{
    public class RetakeResultDto : BaseDto
    {
        public Guid StudentId { get; set; }
        public Guid AttestationId { get; set; }
        public int? Value { get; set; }
        public DateTime Date { get; set; }
    }
}

namespace StudentsManagement.BusinessLogic.Dtos
{
    public class RetakeResultDto : BaseDto
    {
        public string StudentId { get; set; }
        public string AttestationId { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}

namespace StudentsManagement.BusinessLogic.Dtos
{
    public class MarkDto : BaseDto
    {
        public string StudentId { get; set; }
        public string AttestationId { get; set; }
        public int Value { get; set; }
    }
}

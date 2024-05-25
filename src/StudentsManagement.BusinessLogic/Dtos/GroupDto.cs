namespace StudentsManagement.BusinessLogic.Dtos
{
    public class GroupDto : BaseDto
    {
        public string Name { get; set; }
        public int Cource { get; set; }
        public Guid SpecialityId { get; set; }
    }
}

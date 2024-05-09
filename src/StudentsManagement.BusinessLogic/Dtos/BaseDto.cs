namespace StudentsManagement.BusinessLogic.Dtos
{
    public abstract class BaseDto : IDto
    {
        public Guid Id { get; set; }
    }
}

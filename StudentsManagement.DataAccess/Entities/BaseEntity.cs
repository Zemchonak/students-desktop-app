namespace StudentsManagement.DataAccess.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public string Id { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace StudentsManagement.DataAccess.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}

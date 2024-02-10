using System.ComponentModel.DataAnnotations;

namespace StudentsManagement.DataAccess.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        public string Id { get; set; }
    }
}

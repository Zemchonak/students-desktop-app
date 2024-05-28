using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.DataAccess.Entities
{
    [Table("Groups")]
    public class Group : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public int Cource { get; set; }

        [Required]
        public bool Graduated { get; set; }

        [Required]
        public int EnrollYear { get; set; }

        [Required]
        public Guid SpecialityId { get; set; }
    }
}

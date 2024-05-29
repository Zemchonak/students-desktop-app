using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.DataAccess.Entities
{
    [Table("CurriculumUnits")]
    public class CurriculumUnit : BaseEntity
    {
        [Required]
        public Guid SpecialityId { get; set; }

        [Required]
        public int Semester { get; set; }

        [Required]
        public Guid SubjectId { get; set; }

        [Required]
        public Guid WorkTypeId { get; set; }

        public string Name { get; set; }
    }
}

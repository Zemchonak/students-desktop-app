using StudentsManagement.DataAccess.Enums;
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
        public int Cource { get; set; }

        [Required]
        public Guid SubjectId { get; set; }

        [Required]
        public MonitoringType Type { get; set; }
    }
}

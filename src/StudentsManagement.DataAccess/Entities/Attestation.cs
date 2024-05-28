using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.DataAccess.Entities
{
    [Table("Attestations")]
    public class Attestation : BaseEntity
    {
        [Required]
        public Guid? TeacherId { get; set; }

        [Required]
        public Guid? GroupId { get; set; }

        [Required]
        public Guid? CurriculumUnitId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}

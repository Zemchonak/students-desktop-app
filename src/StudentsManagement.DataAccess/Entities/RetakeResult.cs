using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.DataAccess.Entities
{
    [Table("RetakeResults")]
    public class RetakeResult : BaseEntity
    {
        [Required]
        public string StudentId { get; set; }

        [Required]
        public string AttestationId { get; set; }

        public int? Value { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}

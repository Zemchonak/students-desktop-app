using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.DataAccess.Entities
{
    [Table("Specialities")]
    public class Speciality : BaseEntity
    {
        [Required]
        public string ShortName { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}

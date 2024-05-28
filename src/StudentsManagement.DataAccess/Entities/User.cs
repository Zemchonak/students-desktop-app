using StudentsManagement.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.DataAccess.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public bool? IsDisabled { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public string Info { get; set; }

        public Guid? GroupId { get; set; }
    }
}

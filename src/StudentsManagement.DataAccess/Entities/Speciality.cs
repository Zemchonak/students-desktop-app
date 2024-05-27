using StudentsManagement.DataAccess.Enums;
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

        [Required]
        public Guid FacultyId { get; set; }
    }

    [Table("Faculties")]
    public class Faculty : BaseEntity
    {
        [Required]
        public string ShortName { get; set; }

        [Required]
        public string FullName { get; set; }
    }

    [Table("Disciplines")]
    public class Discipline : BaseEntity
    {
        [Required]
        public string ShortName { get; set; }

        [Required]
        public string FullName { get; set; }
    }

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

    [Table("Groups")]
    public class Group : BaseEntity
    {
        [Required]
        public string SpecialityShortName { get; set; }

        public int Cource { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public bool Graduated { get; set; }

        [Required]
        public int EnrollYear { get; set; }

        [Required]
        public Guid SpecialityId { get; set; }
    }

    [Table("CurriculumUnits")]
    public class CurriculumUnit : BaseEntity
    {
        [Required]
        public Guid SpecialityId { get; set; }

        [Required]
        public int Cource { get; set; }

        [Required]
        public Guid DisciplineId { get; set; }

        [Required]
        public MonitoringType Type { get; set; }
    }

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

    [Table("Marks")]
    public class Mark : BaseEntity
    {
        [Required]
        public Guid StudentId { get; set; }

        public Guid AttestationId { get; set; }

        public int? Value { get; set; }
    }

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

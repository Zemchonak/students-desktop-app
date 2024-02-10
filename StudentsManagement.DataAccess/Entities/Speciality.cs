using StudentsManagement.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentsManagement.DataAccess.Entities
{
    public class Speciality : BaseEntity
    {
        [Required]
        public string ShortName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string FacultyId { get; set; }
    }

    public class Faculty : BaseEntity
    {
        [Required]
        public string ShortName { get; set; }

        [Required]
        public string FullName { get; set; }
    }

    public class Discipline : BaseEntity
    {
        [Required]
        public string ShortName { get; set; }

        [Required]
        public string FullName { get; set; }
    }

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

        [Required]
        public UserRole Role { get; set; }

        public string Info { get; set; }

        public string GroupId { get; set; }
    }

    public class Group : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Cource { get; set; }

        [Required]
        public string SpecialityId { get; set; }
    }

    public class CurriculumUnit : BaseEntity
    {
        [Required]
        public string SpecialityId { get; set; }

        [Required]
        public int Cource { get; set; }

        [Required]
        public string DisciplineId { get; set; }

        [Required]
        public MonitoringType Type { get; set; }
    }

    public class Attestation : BaseEntity
    {
        [Required]
        public string TeacherId { get; set; }

        [Required]
        public string GroupId { get; set; }

        [Required]
        public string CurriculumUnitId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }

    public class Mark : BaseEntity
    {
        [Required]
        public string StudentId { get; set; }

        [Required]
        public string AttestationId { get; set; }

        [Required]
        public int Value { get; set; }
    }

    public class RetakeResult : BaseEntity
    {
        [Required]
        public string StudentId { get; set; }

        [Required]
        public string AttestationId { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}

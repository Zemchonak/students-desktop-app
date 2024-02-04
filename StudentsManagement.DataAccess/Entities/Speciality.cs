using StudentsManagement.DataAccess.Enums;

namespace StudentsManagement.DataAccess.Entities
{
    public class Speciality : BaseEntity
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string FacultyId { get; set; }
    }

    public class Faculty : BaseEntity
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
    }

    public class Discipline : BaseEntity
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
    }

    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Info { get; set; }
        public string GroupId { get; set; }
    }

    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public int Cource { get; set; }
        public string SpecialityId { get; set; }
    }

    public class CurriculumUnit : BaseEntity
    {
        public string SpecialityId { get; set; }
        public int Cource { get; set; }
        public string DisciplineId { get; set; }
        public MonitoringType Type { get; set; }
    }

    public class Attestation : BaseEntity
    {
        public string TeacherId { get; set; }
        public string GroupId { get; set; }
        public string CurriculumUnitId { get; set; }
        public DateTime Date { get; set; }
    }

    public class Mark : BaseEntity
    {
        public string StudentId { get; set; }
        public string AttestationId { get; set; }
        public int Value { get; set; }
    }

    public class RetakeResult : BaseEntity
    {
        public string StudentId { get; set; }
        public string AttestationId { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}

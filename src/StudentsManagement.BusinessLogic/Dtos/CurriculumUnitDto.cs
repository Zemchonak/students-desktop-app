using StudentsManagement.DataAccess.Enums;

namespace StudentsManagement.BusinessLogic.Dtos
{
    public class CurriculumUnitDto : BaseDto
    {
        public Guid SpecialityId { get; set; }
        public int Semester { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Guid WorkTypeId { get; set; }
        public string WorkTypeName { get; set; }
        public string Name { get; set; }
    }
}

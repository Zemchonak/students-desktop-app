using StudentsManagement.DataAccess.Enums;

namespace StudentsManagement.BusinessLogic.Dtos
{
    public class CurriculumUnitDto : BaseDto
    {
        public Guid SpecialityId { get; set; }
        public int Cource { get; set; }
        public Guid DisciplineId { get; set; }
        public MonitoringType Type { get; set; }
    }
}

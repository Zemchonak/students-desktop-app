using StudentsManagement.DataAccess.Enums;

namespace StudentsManagement.BusinessLogic.Dtos
{
    public class CurriculumUnitDto : BaseDto
    {
        public string SpecialityId { get; set; }
        public int Cource { get; set; }
        public string DisciplineId { get; set; }
        public MonitoringType Type { get; set; }
    }
}

using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface ICurriculumUnitsService : IService<CurriculumUnitDto>
    {
        public void Validate(CurriculumUnitDto entity);
    }
}

using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface ISpecialitiesService : IService<SpecialityDto>
    {
        public void Validate(SpecialityDto entity);
    }
}

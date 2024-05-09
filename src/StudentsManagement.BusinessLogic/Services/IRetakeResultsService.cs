using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IRetakeResultsService : IService<RetakeResultDto>
    {
        public void Validate(RetakeResultDto entity);
    }
}

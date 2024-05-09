using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class RetakeResultsService : GenericEntityService<RetakeResult, RetakeResultDto>, IRetakeResultsService
    {
        public RetakeResultsService(IRepository<RetakeResult> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Validate(RetakeResultDto entity)
        { }
    }
}

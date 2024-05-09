using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class MarksService : GenericEntityService<Mark, MarkDto>, IMarksService
    {
        public MarksService(IRepository<Mark> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Validate(MarkDto entity)
        { }
    }
}

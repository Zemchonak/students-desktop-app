using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class DisciplinesService : GenericEntityService<Discipline, DisciplineDto>, IDisciplinesService
    {
        public DisciplinesService(IRepository<Discipline> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Validate(DisciplineDto entity)
        { }
    }
}

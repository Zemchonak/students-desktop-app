using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class SubjectsService : GenericEntityService<Subject, SubjectDto>, ISubjectsService
    {
        public SubjectsService(IRepository<Subject> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Validate(SubjectDto entity)
        { }
    }
}

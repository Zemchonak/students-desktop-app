using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class SpecialitiesService : GenericEntityService<Speciality, SpecialityDto>, ISpecialitiesService
    {
        public SpecialitiesService(IRepository<Speciality> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Validate(SpecialityDto entity)
        { }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class FacultiesService : GenericEntityService<Faculty, FacultyDto>, IFacultiesService
    {
        public FacultiesService(IRepository<Faculty> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Validate(FacultyDto entity)
        {
            var faculties = _repository.GetAll(f => f.Id != entity.Id && f.ShortName == entity.ShortName).ToList();

            if(faculties.Count != 0)
            {
                throw new BusinessLogicException("Короткое имя уже занято!");
            }
        }
    }
}

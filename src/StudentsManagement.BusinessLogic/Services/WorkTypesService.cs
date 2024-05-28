using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class WorkTypesService : GenericEntityService<WorkType, WorkTypeDto>, IWorkTypesService
    {
        public WorkTypesService(IRepository<WorkType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Validate(WorkTypeDto entity)
        {
            var items = _repository.GetAll(f => f.Id != entity.Id && f.ShortName == entity.ShortName).ToList();

            if (items.Count != 0)
            {
                throw new BusinessLogicException("Короткое имя уже занято!");
            }
        }
    }
}

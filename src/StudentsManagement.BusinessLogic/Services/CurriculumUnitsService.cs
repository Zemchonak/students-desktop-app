﻿using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class CurriculumUnitsService : GenericEntityService<CurriculumUnit, CurriculumUnitDto>, ICurriculumUnitsService
    {
        public CurriculumUnitsService(IRepository<CurriculumUnit> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IReadOnlyCollection<CurriculumUnitDto> GetUnitsBySpecialityId(Guid specialityId)
        {
            return GetAll(g => g.SpecialityId == specialityId);
        }

        public override void Validate(CurriculumUnitDto entity)
        { }
    }
}

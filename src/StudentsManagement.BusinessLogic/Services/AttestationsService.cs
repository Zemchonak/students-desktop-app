using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class AttestationsService : GenericEntityService<Attestation, AttestationDto>, IAttestationsService
    {
        public AttestationsService(IRepository<Attestation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override void Validate(AttestationDto entity)
        { }
    }
}

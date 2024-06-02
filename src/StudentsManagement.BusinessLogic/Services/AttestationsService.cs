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

        public List<AttestationDto> GetActualAttestationsByGroupId(Guid groupId)
        {
            var dtNow = DateTime.Now.AddMonths(-5); // за семестр

            var items = _repository.GetAll(x =>
                x.Date.Date >= dtNow.Date && x.GroupId == groupId).ToList();

            return _mapper.Map<List<AttestationDto>>(items);
        }

        public List<AttestationDto> GetActualAttestationsByTeacherId(Guid teacherId)
        {
            var dtNow = DateTime.Now.AddMonths(-5); // за семестр

            var items = _repository.GetAll(x =>
                x.Date.Date >= dtNow.Date && x.TeacherId == teacherId).ToList();

            return _mapper.Map<List<AttestationDto>>(items);
        }

        public override void Validate(AttestationDto entity)
        { }
    }
}

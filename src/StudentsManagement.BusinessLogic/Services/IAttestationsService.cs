using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IAttestationsService : IService<AttestationDto>
    {
        public List<AttestationDto> GetActualAttestationsByGroupId(Guid groupId);
        public List<AttestationDto> GetActualAttestationsByTeacherId(Guid teacherId);
    }
}

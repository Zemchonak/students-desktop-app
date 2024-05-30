using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IMarksService : IService<MarkDto>
    {
        void CreateMarks(Guid attestationId, Guid groupId);

        List<MarkDto> GetMarksByAttestationId(Guid attestationId);
    }
}

using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IGroupsService : IService<GroupDto>
    {
        public List<GroupDto> GetActiveGroups(Guid? specialityId = null);
        public IReadOnlyCollection<GroupDto> GetGroupsBySpecialityId(Guid specialityId);
    }
}

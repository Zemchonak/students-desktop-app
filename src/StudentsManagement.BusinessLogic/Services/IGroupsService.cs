using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IGroupsService : IService<GroupDto>
    {
        public List<GroupDto> GetActiveGroups();
        public IReadOnlyCollection<GroupDto> GetGroupsBySpecialityId(Guid specialityId);
    }
}

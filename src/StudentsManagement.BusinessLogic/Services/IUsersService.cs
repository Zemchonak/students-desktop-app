using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.Common.Enums;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IUsersService : IService<UserDto>
    {
        List<UserDto> GetUsersWithRole(UserRole role);
        List<UserDto> GetUsersByGroupId(Guid groupId);
    }
}

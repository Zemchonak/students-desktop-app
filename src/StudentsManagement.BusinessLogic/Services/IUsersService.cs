using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IUsersService : IService<UserDto>
    {
        public Task EnsureAdminExists();
    }
}

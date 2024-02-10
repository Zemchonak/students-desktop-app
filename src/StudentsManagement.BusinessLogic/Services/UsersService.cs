using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Validators;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Enums;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class UsersService : GenericEntityService<User, UserDto>, IUsersService
    {
        private IUsersRepository _usersRepository;

        public UsersService(IValidator<UserDto> validator, IUsersRepository repository, IMapper mapper)
            : base(validator, repository, mapper)
        {
            _usersRepository = repository;
        }

        public async Task EnsureAdminExists()
        {
            try
            {
                await _usersRepository.GetByEmailAsync(Constants.AdminEmail);
            }
            catch (Exception)
            {
                await CreateAdminUser();
            }
        }

        private Task CreateAdminUser()
        {
            return _usersRepository.CreateAsync(new User
            {
                Email = Constants.AdminEmail,
                PasswordHash = Constants.AdminPasswordHash,
                FirstName = Constants.AdminFirstName,
                LastName = Constants.AdminLastName,
                Role = UserRole.Admin
            });
        }
    }
}

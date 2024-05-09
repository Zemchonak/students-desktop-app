using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Enums;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class UsersService : GenericEntityService<User, UserDto>, IUsersService
    {
        private IUsersRepository _usersRepository;

        public UsersService(IUsersRepository repository, IMapper mapper)
        {
            _usersRepository = repository;
            _repository = repository;
            _mapper = mapper;
        }

        public override void Validate(UserDto entity)
        {
            if (string.IsNullOrEmpty(entity.FirstName))
            {
                throw new ValidationException(string.Format(Constants.CannotBeEmptyMessage, "Имя"));
            }

            if (string.IsNullOrEmpty(entity.MiddleName))
            {
                throw new ValidationException(string.Format(Constants.CannotBeEmptyMessage, "Отчество"));
            }

            if (string.IsNullOrEmpty(entity.LastName))
            {
                throw new ValidationException(string.Format(Constants.CannotBeEmptyMessage, "Фамилия"));
            }

            if (string.IsNullOrEmpty(entity.Email))
            {
                throw new ValidationException(string.Format(Constants.CannotBeEmptyMessage, "Email"));
            }

            if (string.IsNullOrEmpty(entity.PasswordHash))
            {
                throw new ValidationException(string.Format(Constants.CannotBeEmptyMessage, "Пароль"));
            }
        }

        public void EnsureAdminExists()
        {
            try
            {
                _usersRepository.GetByEmail(Constants.AdminEmail);
            }
            catch (Exception)
            {
                CreateAdminUser();
            }
        }

        private void CreateAdminUser()
        {
            _usersRepository.Create(new User
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

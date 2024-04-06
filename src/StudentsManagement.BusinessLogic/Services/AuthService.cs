using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IAuthService
    {
        public Task<string> SignIn(string email, string passwordhash);

        public Task<string> Register(string email, string passwordhash);

        public Task<string> ChangePassword(string email, string oldPasswordhash, string newPasswordHash);
    }

    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _userRepository;

        public AuthService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<string> ChangePassword(string email, string oldPasswordhash, string newPasswordHash)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(string email, string passwordhash)
        {
            try
            {
                var createdUserId = await _userRepository.CreateAsync(new User { Email = email, PasswordHash = passwordhash  });

                return createdUserId;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex);
            }
        }

        /// <summary>
        /// Позволяет осуществлять вход в аккаунт
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passwordhash"></param>
        /// <returns>Id пользователя, для которого осуществлён вход</returns>
        /// <exception cref="BusinessLogicException"/>
        public async Task<string> SignIn(string email, string passwordhash)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(email);

                if (user.PasswordHash != passwordhash)
                {
                    throw new BusinessLogicException(Constants.InvalidEmailPasswordMessage);
                }
                else
                {
                    return user.Id;
                }
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException(Constants.InvalidEmailPasswordMessage);
            }
        }
    }
}

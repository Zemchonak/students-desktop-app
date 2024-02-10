using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IAuthService
    {
        public Task<string> SignIn(string email, string passwordhash);

        public Task<string> Register(string user, string passwordhash);

        public Task<string> ChangePassword(string user, string oldPasswordhash, string newPasswordHash);
    }

    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _userRepository;

        public AuthService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<string> ChangePassword(string user, string oldPasswordhash, string newPasswordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> Register(string user, string passwordhash)
        {
            throw new NotImplementedException();
        }

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

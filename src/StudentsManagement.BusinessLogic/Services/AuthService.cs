using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{

    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _userRepository;

        public AuthService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ChangePassword(string email, string oldPasswordhash, string newPasswordHash)
        {
            throw new NotImplementedException();
        }

        public Guid Register(string email, string passwordhash)
        {
            try
            {
                var createdUserId = _userRepository.Create(new User { Email = email, PasswordHash = passwordhash  });

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
        public Guid SignIn(string email, string passwordhash)
        {
            try
            {
                var user = _userRepository.GetByEmail(email);

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

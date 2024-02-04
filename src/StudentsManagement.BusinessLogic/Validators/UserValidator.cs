using StudentsManagement.BusinessLogic.Dtos;

namespace StudentsManagement.BusinessLogic.Validators
{
    public class UserValidator : IValidator<UserDto>
    {
        public Task Validate(UserDto entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

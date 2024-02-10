using Microsoft.Extensions.DependencyInjection;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.BusinessLogic.Validators;
using StudentsManagement.DataAccess;
using StudentsManagement.DataAccess.Entities;

namespace StudentsManagement.BusinessLogic
{
    public static class DiExtension
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services, string connectionString)
        {
            return services
                .AddDataAccessServices(connectionString)

                .AddTransient<IValidator<UserDto>, UserValidator>()
                //.AddTransient<IValidator<>, CategoryValidator>()

                .AddTransient<IUsersService, UsersService>()

                .AddTransient<IAuthService, AuthService>();
        }
    }
}

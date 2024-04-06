using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.DataAccess
{
    public static class DiExtension
    {
        public static IServiceCollection AddDataAccessServices(
            this IServiceCollection services,
            string connectionString)
        {
            return services
                .AddDbContext<StudentsAppContext>(options =>
                {
                    options.UseSqlServer(connectionString, builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    });
                })
                .AddScoped(typeof(IRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUsersRepository, UsersEntityFrameworkRepository>();
        }
    }
}

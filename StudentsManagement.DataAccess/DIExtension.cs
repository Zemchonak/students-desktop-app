using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.DataAccess
{
    public static class DiExtension
    {
        public static IServiceCollection AddDataAccessServices(
            this IServiceCollection services,
            string connectionString = null)
        {
            return services
                .AddDbContext<StudentsAppContext>(options =>
                {
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        options.UseInMemoryDatabase("StudentsDb");
                    }
                    else
                    {
                        options.UseSqlServer(connectionString);
                        // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    }
                })
                .AddScoped(typeof(IRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUsersRepository, UsersEntityFrameworkRepository>();
        }
    }
}

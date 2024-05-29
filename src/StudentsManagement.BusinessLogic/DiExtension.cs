using Microsoft.Extensions.DependencyInjection;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DataAccess;

namespace StudentsManagement.BusinessLogic
{
    public static class DiExtension
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services, string connectionString = null)
        {
            return services
                .AddDataAccessServices(connectionString)

                .AddTransient<IUsersService, UsersService>()
                .AddTransient<IAuthService, AuthService>()

                .AddTransient<IWorkTypesService, WorkTypesService>()
                .AddTransient<ISpecialitiesService, SpecialitiesService>()
                .AddTransient<ISubjectsService, SubjectsService>()
                .AddTransient<ICurriculumUnitsService, CurriculumUnitsService>()
                .AddTransient<IGroupsService, GroupsService>()
                .AddTransient<IAttestationsService, AttestationsService>()
                .AddTransient<IMarksService, MarksService>()
                .AddTransient<IRetakeResultsService, RetakeResultsService>();
        }
    }
}

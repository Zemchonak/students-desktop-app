using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsManagement.BusinessLogic;
using StudentsManagement.BusinessLogic.Mapper;
using StudentsManagement.BusinessLogic.Services;
using StudentsManagement.DataAccess;
using StudentsManagement.DataAccess.Repositories;
using StudentsManagement.DesktopApp.AuthWindows;
using StudentsManagement.DesktopApp.Mapper;
using StudentsManagement.DesktopApp.Utils;
using System;
using System.Threading.Tasks;

namespace StudentsManagement.DesktopApp
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    var mapperConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new BusinessLogicMappingProfile());
                        mc.AddProfile(new DesktopAppMappingProfile());
                    });

                    var mapper = mapperConfig.CreateMapper();
                    services.AddSingleton(mapper);

                    var connectionString = "Server=tcp:studentsmanagement.database.windows.net,1433;Initial Catalog=studentsmanagement-db;Persist Security Info=False;" +
                    "User ID=aaddmin_1Db;Password=jt0Xlb#r2A#zkVPT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                    services.AddBusinessLogicServices(connectionString);

                    var dbOptions = new DbContextOptionsBuilder<StudentsAppContext>().UseSqlServer(connectionString);

                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<LoginWindow>();

                })
                .Build();

            Task.Run(async () =>
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var usersRepository = services.GetRequiredService<IUsersRepository>();
                    var usersService = services.GetRequiredService<IUsersService>();

                    await DatabaseInitializer.EnsureAdminExistsAsync(usersService);
                }
            });

            var app = host.Services.GetService<App>();

            app?.Run();
        }
    }
}

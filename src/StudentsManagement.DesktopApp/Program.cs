using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsManagement.BusinessLogic;
using StudentsManagement.BusinessLogic.Mapper;
using StudentsManagement.DesktopApp.Mapper;
using System;
using System.IO;

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
                    var configuration = SetupSettingsJson();
                    services.AddSingleton<IConfiguration>(configuration);

                    var connectionString = configuration.GetConnectionString("StudentsDb");

                    var mapperConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new BusinessLogicMappingProfile());
                        mc.AddProfile(new DesktopAppMappingProfile());
                    });

                    var mapper = mapperConfig.CreateMapper();
                    services.AddSingleton(mapper);

                    services.AddBusinessLogicServices(connectionString);

                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                })
                .Build();

            var app = host.Services.GetService<App>();

            app?.Run();
        }

        protected static IConfiguration SetupSettingsJson()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}

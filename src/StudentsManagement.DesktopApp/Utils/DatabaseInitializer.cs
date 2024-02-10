using StudentsManagement.BusinessLogic.Services;
using System.Threading.Tasks;

namespace StudentsManagement.DesktopApp.Utils
{
    public static class DatabaseInitializer
    {
        public static async Task EnsureAdminExistsAsync(IUsersService usersService)
        {
            await usersService.EnsureAdminExists();
        }
    }
}

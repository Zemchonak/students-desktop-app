using StudentsManagement.BusinessLogic.Services;
using System.Threading.Tasks;

namespace StudentsManagement.DesktopApp.Utils
{
    public static class DatabaseInitializer
    {
        public static void EnsureAdminExists(IUsersService usersService)
        {
            usersService.EnsureAdminExists();
        }
    }
}

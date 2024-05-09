using StudentsManagement.DataAccess.Entities;

namespace StudentsManagement.DataAccess.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        public User GetByEmail(string email);
        public void EnsureUsersTableAvailable();
    }
}

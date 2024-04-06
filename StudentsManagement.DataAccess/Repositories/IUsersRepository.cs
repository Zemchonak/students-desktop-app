using StudentsManagement.DataAccess.Entities;

namespace StudentsManagement.DataAccess.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        public Task EnsureUsersTableAvailable();
    }
}

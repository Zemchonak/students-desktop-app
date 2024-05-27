using Microsoft.EntityFrameworkCore;
using StudentsManagement.DataAccess.Entities;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace StudentsManagement.DataAccess.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        private readonly StudentsAppContext _context;

        public UsersRepository(StudentsAppContext context)
            : base(context)
        {
            _context = context;
        }
        public User GetByEmail(string email)
        {
            var entity = _context.Set<User>()
                .FirstOrDefault(u => u.Email == email)
                ?? throw new ArgumentException(nameof(email));

            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }
    }
}

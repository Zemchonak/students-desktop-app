using Microsoft.EntityFrameworkCore;
using StudentsManagement.DataAccess.Entities;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace StudentsManagement.DataAccess.Repositories
{
    public class UsersEntityFrameworkRepository : GenericRepository<User>, IUsersRepository
    {
        private readonly StudentsAppContext _context;

        public UsersEntityFrameworkRepository(StudentsAppContext context)
            : base(context)
        {
            _context = context;
        }

        public void EnsureUsersTableAvailable()
        {
            _context.Database.EnsureCreated();
        }

        public override Guid Create(User entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;

            return entity.Id;
        }

        public override User GetById(Guid entityId)
        {
            var entity = _context.Set<User>()
                .FirstOrDefault(e => e.Id == entityId)
                ?? throw new ArgumentException(nameof(entityId));

            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public override IQueryable<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            var entities = _context.Set<User>();

            if (filter != null)
            {
                return entities.Where(filter).AsNoTracking();
            }
            else
            {
                return entities.AsNoTracking();
            }
        }

        public override void Update(User entity)
        {
            if (!_context.Set<User>().Any(e => e.Id == entity.Id))
            {
                throw new ArgumentException(nameof(entity));
            }

            _context.Update(entity);
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public override void Delete(Guid entityId)
        {
            var entity = GetById(entityId)
                ?? throw new ArgumentException(nameof(entityId));

            _context.Remove(entity);
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
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

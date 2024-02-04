using Microsoft.EntityFrameworkCore;
using StudentsManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.DataAccess.Repositories
{
    public class UsersEntityFrameworkRepository : GenericRepository<User>
    {
        private readonly StudentsAppContext _context;

        public UsersEntityFrameworkRepository(StudentsAppContext context)
            : base(context)
        {
        }

        public override async Task<string> CreateAsync(User entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _context.Entry(entity).State = EntityState.Detached;
            return entity.Id;
        }

        public override async Task<User> GetByIdAsync(string entityId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Set<User>()
                .FirstOrDefaultAsync(e => e.Id == entityId, cancellationToken)
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

        public override async Task UpdateAsync(User entity, CancellationToken cancellationToken = default)
        {
            if (!_context.Set<User>().Any(e => e.Id == entity.Id))
            {
                throw new ArgumentException(nameof(entity));
            }

            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _context.Entry(entity).State = EntityState.Detached;
        }

        public override async Task DeleteAsync(string entityId, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(entityId, cancellationToken)
                ?? throw new ArgumentException(nameof(entityId));

            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}

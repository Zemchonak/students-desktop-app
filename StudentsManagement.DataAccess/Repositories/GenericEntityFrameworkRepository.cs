using Microsoft.EntityFrameworkCore;
using StudentsManagement.DataAccess.Entities;
using System.Linq.Expressions;

namespace StudentsManagement.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
         where TEntity : class, IEntity
    {
        private readonly StudentsAppContext _context;

        public GenericRepository(StudentsAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<string> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _context.Entry(entity).State = EntityState.Detached;
            return entity.Id;
        }

        public virtual async Task<TEntity> GetByIdAsync(string entityId, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == entityId, cancellationToken)
                ?? throw new ArgumentException(nameof(entityId));

            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            var entities = _context.Set<TEntity>();

            if (filter != null)
            {
                return entities.Where(filter).AsNoTracking();
            }
            else
            { 
                return entities.AsNoTracking();
            }
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!_context.Set<TEntity>().Any(e => e.Id == entity.Id))
            {
                throw new ArgumentException(nameof(entity));
            }

            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _context.Entry(entity).State = EntityState.Detached;
        }

        public virtual async Task DeleteAsync(string entityId, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(entityId, cancellationToken)
                ?? throw new ArgumentException(nameof(entityId));

            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}

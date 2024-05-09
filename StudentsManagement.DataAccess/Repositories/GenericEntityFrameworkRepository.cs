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

        public virtual Guid Create(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;

            return entity.Id;
        }

        public virtual TEntity GetById(Guid entityId)
        {
            var entity = _context.Set<TEntity>()
                .FirstOrDefault(e => e.Id == entityId)
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

        public virtual void Update(TEntity entity)
        {
            if (!_context.Set<TEntity>().Any(e => e.Id == entity.Id))
            {
                throw new ArgumentException(null, nameof(entity));
            }

            _context.Update(entity);
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public virtual void Delete(Guid entityId)
        {
            var entity = GetById(entityId)
                ?? throw new ArgumentException(nameof(entityId));

            _context.Remove(entity);
            _context.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}

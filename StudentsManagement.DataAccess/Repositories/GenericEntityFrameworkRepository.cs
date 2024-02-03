using StudentsManagement.DataAccess.Entities;

namespace StudentsManagement.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
         where TEntity : class, IEntity
    {
        private readonly CatalogServiceContext _catalogContext;

        public GenericRepository(CatalogServiceContext catalogContext)
        {
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _catalogContext.AddAsync(entity, cancellationToken);
            await _catalogContext.SaveChangesAsync(cancellationToken);
            _catalogContext.Entry(entity).State = EntityState.Detached;
            return entity.Id;
        }

        public async Task<TEntity> GetByIdAsync(int entityId, CancellationToken cancellationToken = default)
        {
            var entity = await _catalogContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == entityId, cancellationToken)
                ?? throw new ArgumentException(nameof(entityId));

            _catalogContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _catalogContext.Set<TEntity>().AsNoTracking();
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!_catalogContext.Set<TEntity>().Any(e => e.Id == entity.Id))
            {
                throw new ArgumentException(nameof(entity));
            }

            _catalogContext.Update(entity);
            await _catalogContext.SaveChangesAsync(cancellationToken);
            _catalogContext.Entry(entity).State = EntityState.Detached;
        }

        public async Task DeleteAsync(int entityId, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(entityId, cancellationToken)
                ?? throw new ArgumentException(nameof(entityId));

            _catalogContext.Remove(entity);
            await _catalogContext.SaveChangesAsync(cancellationToken);
            _catalogContext.Entry(entity).State = EntityState.Detached;
        }
    }
}

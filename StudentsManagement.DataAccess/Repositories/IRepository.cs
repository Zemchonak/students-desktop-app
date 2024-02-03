using System.Security.Principal;

namespace StudentsManagement.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int entityId, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(int entityId, CancellationToken cancellationToken = default);
    }
}
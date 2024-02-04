using StudentsManagement.DataAccess.Entities;
using System.Linq.Expressions;

namespace StudentsManagement.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<string> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity,bool>> filter = null);

        Task<TEntity> GetByIdAsync(string entityId, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(string entityId, CancellationToken cancellationToken = default);
    }
}
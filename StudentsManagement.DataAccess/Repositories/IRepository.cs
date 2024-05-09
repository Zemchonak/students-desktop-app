using StudentsManagement.DataAccess.Entities;
using System.Linq.Expressions;

namespace StudentsManagement.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        Guid Create(TEntity entity);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity,bool>> filter = null);

        TEntity GetById(Guid entityId);

        void Update(TEntity entity);

        void Delete(Guid entityId);
    }
}
using StudentsManagement.BusinessLogic.Dtos;
using System.Linq.Expressions;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IService<TEntityDto>
        where TEntityDto : class, IDto
    {
        Task<string> CreateAsync(TEntityDto entity, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<TEntityDto>> GetAllAsync(Expression<Func<TEntityDto, bool>> filter = null,
            CancellationToken cancellationToken = default);

        Task<TEntityDto> GetById(string entityId, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntityDto entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(string entityId, CancellationToken cancellationToken = default);
    }
}

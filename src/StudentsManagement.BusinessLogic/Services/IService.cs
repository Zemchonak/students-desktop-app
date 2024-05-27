using StudentsManagement.BusinessLogic.Dtos;
using System.Linq.Expressions;

namespace StudentsManagement.BusinessLogic.Services
{
    public interface IService<TEntityDto>
        where TEntityDto : class, IDto
    {
        Guid Create(TEntityDto entity);

        IReadOnlyCollection<TEntityDto> GetAll(Func<TEntityDto, bool> filter = null);

        TEntityDto GetById(Guid entityId);

        void Update(TEntityDto entity);

        void Delete(Guid entityId);

        void Validate(TEntityDto entity);
    }
}

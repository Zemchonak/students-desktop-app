using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.BusinessLogic.Validators;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;
using System.Linq.Expressions;

namespace StudentsManagement.BusinessLogic.Services
{
    public class GenericEntityService<TEntity, TEntityDto> : IService<TEntityDto>
        where TEntity : class, IEntity
        where TEntityDto : class, IDto
    {
        private protected readonly IValidator<TEntityDto> _validator;
        private protected readonly IRepository<TEntity> _repository;
        private protected readonly IMapper _mapper;

        public GenericEntityService(IValidator<TEntityDto> validator, IRepository<TEntity> repository, IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }

        public Task<string> CreateAsync(TEntityDto entity, CancellationToken cancellationToken = default)
        {
            _validator.Validate(entity);
            return _repository.CreateAsync(_mapper.Map<TEntity>(entity), cancellationToken);
        }

        public Task DeleteAsync(string entityId, CancellationToken cancellationToken = default)
        {
            try
            {
                return _repository.DeleteAsync(entityId, cancellationToken);
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException($"There is no {nameof(TEntity)} found by Id='{entityId}' to delete.");
            }
        }

        public async Task<IReadOnlyCollection<TEntityDto>> GetAllAsync(Expression<Func<TEntityDto, bool>> filter = null,
            CancellationToken cancellationToken = default)
        {
            var allItems = await _repository.GetAll().ToListAsync();

            return _mapper.Map<List<TEntityDto>>(allItems.AsReadOnly());
        }

        public async Task<TEntityDto> GetById(string entityId, CancellationToken cancellationToken = default)
        {
            try
            {
                return _mapper.Map<TEntityDto>(
                    await _repository.GetByIdAsync(entityId, cancellationToken));
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException($"There is no {nameof(TEntity)} found by Id='{entityId}'.");
            }
        }

        public Task UpdateAsync(TEntityDto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                _validator.Validate(entity, cancellationToken);
                var mappedEntity = _mapper.Map<TEntity>(entity);

                return _repository.CreateAsync(mappedEntity, cancellationToken);
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException($"There is no {nameof(TEntity)} found by Id='{entity.Id}' to update.");
            }
        }
    }
}

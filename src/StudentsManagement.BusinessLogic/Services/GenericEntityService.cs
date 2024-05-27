using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.BusinessLogic.Exceptions;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;
using System.Linq.Expressions;

namespace StudentsManagement.BusinessLogic.Services
{
    public abstract class  GenericEntityService<TEntity, TEntityDto> : IService<TEntityDto>
        where TEntity : class, IEntity
        where TEntityDto : class, IDto
    {
        private protected IRepository<TEntity> _repository;
        private protected IMapper _mapper;

        public Guid Create(TEntityDto entity)
        {
            return _repository.Create(_mapper.Map<TEntity>(entity));
        }

        public IReadOnlyCollection<TEntityDto> GetAll(Func<TEntityDto, bool> filter = null)
        {
            var allItems = _mapper.Map<List<TEntityDto>>(_repository.GetAll().ToList());

            if(filter != null)
            {
                return allItems.Where(filter).ToList().AsReadOnly();
            }
            else
            {
                return allItems.AsReadOnly();
            }
        }

        public TEntityDto GetById(Guid entityId)
        {
            try
            {
                return _mapper.Map<TEntityDto>(
                    _repository.GetById(entityId));
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException($"There is no {nameof(TEntity)} found by Id='{entityId}'.");
            }
        }

        public void Update(TEntityDto entity)
        {
            try
            {
                var mappedEntity = _mapper.Map<TEntity>(entity);

                _repository.Update(mappedEntity);
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException($"There is no {nameof(TEntity)} found by Id='{entity.Id}' to update.");
            }
        }

        public void Delete(Guid entityId)
        {
            try
            {
                _repository.Delete(entityId);
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException($"There is no {nameof(TEntity)} found by Id='{entityId}' to delete.");
            }
        }

        public abstract void Validate(TEntityDto entity);
    }
}

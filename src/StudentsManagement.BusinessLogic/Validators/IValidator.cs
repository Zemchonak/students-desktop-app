namespace StudentsManagement.BusinessLogic.Validators
{
    public interface IValidator<TEntityDto>
        where TEntityDto : class
    {
        public Task Validate(TEntityDto entity, CancellationToken cancellationToken = default);
    }
}

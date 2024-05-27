using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class GroupsService : GenericEntityService<Group, GroupDto>, IGroupsService
    {
        public GroupsService(IRepository<Group> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IReadOnlyCollection<GroupDto> GetGroupsBySpecialityId(Guid specialityId)
        {
            return GetAll(g => g.SpecialityId == specialityId);
        }

        public override void Validate(GroupDto entity)
        { }
    }
}

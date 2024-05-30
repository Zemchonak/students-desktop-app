using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;
using StudentsManagement.DataAccess.Repositories;

namespace StudentsManagement.BusinessLogic.Services
{
    public class MarksService : GenericEntityService<Mark, MarkDto>, IMarksService
    {
        private readonly IRepository<User> _userRepository;

        public MarksService(IRepository<Mark> repository, IRepository<User> userRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void CreateMarks(Guid attestationId, Guid groupId)
        {
            var groupUsers = _userRepository.GetAll(u =>
                u.GroupId.Value == groupId
                && !u.IsDisabled.Value).ToList();

            var marks = groupUsers.Select(x => new Mark { AttestationId = attestationId, StudentId = x.Id, NotAllowed = false, NotAttended = false, Value = null });

            foreach (var mark in marks)
            {
                _repository.Create(mark);
            }
        }

        public List<MarkDto> GetMarksByAttestationId(Guid attestationId)
        {
            return _mapper.Map<List<MarkDto>>(_repository.GetAll(x => x.AttestationId == attestationId));
        }

        public override void Validate(MarkDto entity)
        { }
    }
}

using AutoMapper;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;

namespace StudentsManagement.BusinessLogic.Mapper
{
    public class BusinessLogicMappingProfile : Profile
    {
        public BusinessLogicMappingProfile()
        {
            CreateMap<Speciality, SpecialityDto>().ReverseMap();
            CreateMap<WorkType, WorkTypeDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<CurriculumUnit, CurriculumUnitDto>().ReverseMap();
            CreateMap<Attestation, AttestationDto>().ReverseMap();
            CreateMap<Mark, MarkDto>().ReverseMap();
            CreateMap<RetakeResult, RetakeResultDto>().ReverseMap();

        }
    }
}

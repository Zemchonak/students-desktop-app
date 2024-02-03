using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using StudentsManagement.BusinessLogic.Dtos;
using StudentsManagement.DataAccess.Entities;

namespace StudentsManagement.BusinessLogic.Mapper
{
    public class BusinessLogicMappingProfile : Profile
    {
        public BusinessLogicMappingProfile()
        {
            CreateMap<Speciality, SpecialityDto>().ReverseMap();
            CreateMap<Faculty, FacultyDto>().ReverseMap();
            CreateMap<Discipline, DisciplineDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<CurriculumUnit, CurriculumUnitDto>().ReverseMap();
            CreateMap<Attestation, AttestationDto>().ReverseMap();
            CreateMap<Mark, MarkDto>().ReverseMap();
            CreateMap<RetakeResult, RetakeResultDto>().ReverseMap();

            CreateMap<DataAccess.Enums.MonitoringType, BusinessLogic.Enums.MonitoringType>()
                .ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();
            CreateMap<DataAccess.Enums.UserRole, BusinessLogic.Enums.UserRole>()
                .ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

        }
    }
}

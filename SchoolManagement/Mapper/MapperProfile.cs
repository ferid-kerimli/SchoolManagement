using AutoMapper;
using SchoolManagement.DTO.SchoolDTOs;
using SchoolManagement.DTO.StudentDTOs;
using SchoolManagement.DTO.UserDto;
using SchoolManagement.Entities;
using SchoolManagement.Entities.Identity;

namespace SchoolManagement.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<School, SchoolGetDTO>().ReverseMap();
            CreateMap<School, SchoolCreateDTO>().ReverseMap();
            CreateMap<School, SchoolUpdateDTO>().ReverseMap();
            
            CreateMap<StudentCreateDTO, Student>().ReverseMap();
            CreateMap<Student, StudentGetDTO>().ReverseMap();
            CreateMap<StudentUpdateDTO, Student>().ReverseMap();

            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<CreateUserResponseDto, User>().ReverseMap();
            CreateMap<UserGetDto, User>().ReverseMap();
            CreateMap<UserUpdateDto, User>().ReverseMap();
        }
    }
}

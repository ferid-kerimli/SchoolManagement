using AutoMapper;
using SchoolManagement.DTO.SchoolDTOs;
using SchoolManagement.DTO.StudentDTOs;
using SchoolManagement.Entities;

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
        }
    }
}

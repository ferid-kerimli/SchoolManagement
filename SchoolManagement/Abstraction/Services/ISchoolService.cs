using SchoolManagement.DTO.SchoolDTOs;
using SchoolManagement.Models;

namespace SchoolManagement.Abstraction.Services
{
    public interface ISchoolService
    {
        Task<GenericResponseModel<List<SchoolGetDTO>>> GetAllSchool();
        Task<GenericResponseModel<SchoolGetDTO>> GetSchoolById(int id);
        Task<GenericResponseModel<SchoolCreateDTO>> AddSchool(SchoolCreateDTO schoolCreateDTO);
        Task<GenericResponseModel<bool>> UpdateSchool(SchoolUpdateDTO schoolUpdateDTO);
        Task<GenericResponseModel<bool>> DeleteSchool(int id);
    }
}

using SchoolManagement.DTO.StudentDTOs;
using SchoolManagement.Models;

namespace SchoolManagement.Abstraction.Services
{
    public interface IStudentService
    {
        Task<GenericResponseModel<bool>> CreateStudent(StudentCreateDTO studentCreateDto);

        Task<GenericResponseModel<bool>> UpdateStudent(StudentUpdateDTO studentUpdateDto, int id);
        Task<GenericResponseModel<bool>> DeleteStudent(int id);
        Task<GenericResponseModel<StudentGetDTO>> GetStudentById(int id);

        Task<GenericResponseModel<bool>> ChangeSchool(int studentId, int newSchoolId);
        Task<GenericResponseModel<bool>> ChangeSchool(ChangeSchoolDTO changeSchoolDto);
        Task<GenericResponseModel<List<StudentGetDTO>>> GetAllStudents();
    }
}

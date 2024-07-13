using SchoolManagement.Entities;

namespace SchoolManagement.DTO.StudentDTOs
{
    public class StudentCreateDTO
    {
        public string FullName { get; set; }
        public School School { get; set; }
    }
}

using SchoolManagement.Abstraction.Repositories.StudentRepository;
using SchoolManagement.Context;
using SchoolManagement.Entities;

namespace SchoolManagement.Implementation.Repositories.EntitiesRepositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDBContext _context) : base(_context)
        {
        }
    }
}

using SchoolManagement.Abstraction.Repositories.SchoolRepository;
using SchoolManagement.Context;
using SchoolManagement.Entities;

namespace SchoolManagement.Implementation.Repositories.EntitiesRepositories
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDBContext _context) : base(_context)
        {
        }
    }
}

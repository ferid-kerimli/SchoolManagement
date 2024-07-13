using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Entities;
using SchoolManagement.Entities.Identity;

namespace SchoolManagement.Context
{
    public class ApplicationDBContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {            
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }    
    }
}

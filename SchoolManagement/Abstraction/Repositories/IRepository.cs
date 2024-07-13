using Microsoft.EntityFrameworkCore;
using SchoolManagement.Entities;

namespace SchoolManagement.Abstraction.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T data);
        bool Remove(T data);
        Task<bool> RemoveByID(int id);
        bool Update(T data);
    }
}

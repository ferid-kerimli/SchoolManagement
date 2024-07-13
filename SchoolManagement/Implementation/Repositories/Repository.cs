using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolManagement.Abstraction.Repositories;
using SchoolManagement.Context;
using SchoolManagement.Entities;

namespace SchoolManagement.Implementation.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext _context;
        public Repository(ApplicationDBContext _context)
        {
            this._context = _context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T data)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(data);
            return entityEntry.State == EntityState.Added;
        }

        public IQueryable<T> GetAll()
        {
            var query = Table.AsQueryable();
            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = Table.AsQueryable();
            return await query.FirstOrDefaultAsync(data => data.Id == id);
        }

        public bool Remove(T data)
        {
            EntityEntry<T> entityEntry = Table.Remove(data);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveByID(int id)
        {
            T data = await Table.FindAsync(id);
            return Remove(data);
        }

        public bool Update(T data)
        {
            EntityEntry<T> entityEntry = Table.Update(data);
            return entityEntry.State == EntityState.Modified;
        }
    }
}

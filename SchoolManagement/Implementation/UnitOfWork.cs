using SchoolManagement.Abstraction;
using SchoolManagement.Abstraction.Repositories;
using SchoolManagement.Context;
using SchoolManagement.Entities;
using SchoolManagement.Implementation.Repositories;

namespace SchoolManagement.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public IRepository<T> GetRepository<T>() where T : BaseEntity
    {
        if (_repositories.ContainsKey(typeof(T)))
        {
            return (IRepository<T>)_repositories[typeof(T)];
        }

        IRepository<T> repository = new Repository<T>(_context);
        _repositories.Add(typeof(T), repository);
        return repository;
    }

    public async Task<int> Commit()
    {
        return await _context.SaveChangesAsync();
    }
}